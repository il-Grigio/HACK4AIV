using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClientOrderMGR : Grigios.Singleton<ClientOrderMGR>
{

    [System.Serializable]
    class RecepiePhase {
        public Recepie[] possibleRecepies;
        public float timeToFinishPhase = 180f;
        public float currentTime = 0;
        public float timeToAddRecepie = 60;
        public int initialRecepiesInThisPhase;
        public int totalRecepiesInThisPhase;
    }

    [SerializeField] RecepiePhase[] myPhases;

    [Header("Events")]
    [SerializeField] UnityEvent newRecepieEntering;
    [SerializeField] UnityEvent recepieFinished;
    [SerializeField] UnityEvent newPhase;
    [SerializeField] UnityEvent deliveredWrongRecepy;
    [SerializeField] UnityEvent didntFinishInTimeRecepie;
    [SerializeField] UnityEvent win;
    [SerializeField] UnityEvent lose;

    [Header("DEbug")]
    [SerializeField] bool nextPhase = false;
    public List<Recepie> activeRecepies;

    //se scade il tempo di una ricetta ++
    public int failedRecepies = 0;


    //N ricette iniziali
    //parte il tempo per finire le N ricette
    //finite tutte le N ricette il tempo aumenta
    //N=N+M         change phase
    //arrivano nuove N ricette


    //ogni ricetta ha un suo tempo per essere completato


    //private
    int currentPhaseIndex = -1;
    public int CurrentPhaseIndex => currentPhaseIndex;
    List<Recepie> recepiesToRemove = new List<Recepie>();
    float partialTime;
    int nRecepiesArrived = 0;
    UIClientOrder uiClientOrder;
    UIGeneralTimer uiTimer;

    private void Start() {
        failedRecepies = 0;
        activeRecepies.Clear();
        GoToNextPhase();
        GameObject t = GameObject.Find("Timer");
        uiTimer = t.GetComponent<UIGeneralTimer>();
        if(uiTimer)
            uiTimer.SetTimer(myPhases[currentPhaseIndex].timeToFinishPhase, myPhases[currentPhaseIndex].timeToFinishPhase);

    }
    public void GoToNextPhase() {
        currentPhaseIndex++;
        nRecepiesArrived = 0;
        failedRecepies = 0;
        newPhase.Invoke();
        for (int i = 0; i < myPhases[currentPhaseIndex].initialRecepiesInThisPhase; i++) {
            AddNewRecepie();
        }
        myPhases[currentPhaseIndex].currentTime = myPhases[currentPhaseIndex].timeToFinishPhase;
        if (uiTimer) uiTimer.SetTimer(myPhases[currentPhaseIndex].currentTime, myPhases[currentPhaseIndex].timeToFinishPhase);
        partialTime = 0;
    }

    private void Update() {
        if (nextPhase) {
            nextPhase = false;
            GoToNextPhase();
        }

        myPhases[currentPhaseIndex].currentTime -= Time.deltaTime;
        if (myPhases[currentPhaseIndex].currentTime < 0) {
            lose.Invoke();
        }

        if (nRecepiesArrived < myPhases[currentPhaseIndex].totalRecepiesInThisPhase) {

            partialTime += Time.deltaTime;
            if((partialTime > myPhases[currentPhaseIndex].timeToAddRecepie || activeRecepies.Count <= 1) && activeRecepies.Count < 5) {
                //Add recepie
                partialTime = 0;
                AddNewRecepie();
            }
        }

        if(activeRecepies.Count == 0) {
            if(failedRecepies < Mathf.CeilToInt(myPhases[currentPhaseIndex].totalRecepiesInThisPhase * 0.5f)) {
                GoToNextPhase();
            }
            else {
                lose.Invoke();
            }
        }


        foreach (Recepie recepie in activeRecepies) {
            recepie.currentTime -= Time.deltaTime;
            if(recepie.currentTime <= 0) {
                myPhases[currentPhaseIndex].currentTime -= recepie.timeLostOnIncomplete;
                if (uiTimer) uiTimer.SetTimer(myPhases[currentPhaseIndex].currentTime, myPhases[currentPhaseIndex].timeToFinishPhase);
                didntFinishInTimeRecepie.Invoke();
                failedRecepies++;
                recepiesToRemove.Add(recepie);
            }
        }
        if(recepiesToRemove.Count > 0) {
            foreach(var item in recepiesToRemove)
            {
                uiClientOrder.RemoveItem(item);
                activeRecepies.Remove(item);
                Destroy(item);
            }
            //activeRecepies.RemoveAll(i => recepiesToRemove.Contains(i));
            recepiesToRemove.Clear();

        }
    }

    public void DeliverAnItem(IngredientScriptable item) {
        
        foreach (Recepie recepie in activeRecepies) {
            if(recepie.recepie == item) {
                DeliverARecepie(recepie);
                return;
            }
        }
    }

    public void DeliverARecepie(Recepie recepie) {
        if (recepie == null) { return; }
        if(activeRecepies.Contains(recepie)) {
            activeRecepies.Remove(recepie);
            uiClientOrder.RemoveItem(recepie);
            myPhases[currentPhaseIndex].currentTime += recepie.timeBonusOnComplete * recepie.currentTime / recepie.timeToFinishRecepie;
            if (uiTimer) uiTimer.SetTimer(myPhases[currentPhaseIndex].currentTime, myPhases[currentPhaseIndex].timeToFinishPhase);
            recepieFinished.Invoke();

            if(activeRecepies.Count == 0) {
                GoToNextPhase();
            }
        } else {
            deliveredWrongRecepy.Invoke();
        }
    }

    public void AddNewRecepie() {
        RecepiePhase currentPhase = myPhases[currentPhaseIndex];
        if (currentPhase != null) {
            Recepie newRecepie = Instantiate( currentPhase.possibleRecepies[Random.Range(0, currentPhase.possibleRecepies.Length)]);
            activeRecepies.Add(newRecepie);
            nRecepiesArrived++;
            newRecepie.currentTime = newRecepie.timeToFinishRecepie;

            if(!uiClientOrder)
            {
                uiClientOrder = GameObject.Find("ToysMenu").GetComponent<UIClientOrder>();
            }
            uiClientOrder.NewRequest(newRecepie);
        }
        else {
            //TODO finishGame
            win.Invoke();
        }
        newRecepieEntering.Invoke();
    }
}
