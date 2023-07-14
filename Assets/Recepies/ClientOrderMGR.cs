using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
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
    int currentPhaseIndex = 1;
    public int CurrentPhaseIndex => currentPhaseIndex;
    List<Recepie> recepiesToRemove = new List<Recepie>();
    float partialTime;
    int nRecepiesArrived = 0;
    private void Start() {
        failedRecepies = 0;
        activeRecepies.Clear();
    }
    public void GoToNextPhase() {
        currentPhaseIndex++;
        nRecepiesArrived = 0;
        newPhase.Invoke();
        for (int i = 0; i < myPhases[currentPhaseIndex].initialRecepiesInThisPhase; i++) {
            AddNewRecepie();
        }
        myPhases[currentPhaseIndex].currentTime = myPhases[currentPhaseIndex].timeToFinishPhase;
        partialTime = 0;
    }

    private void Update() {
        myPhases[currentPhaseIndex].currentTime -= Time.deltaTime;
        if (myPhases[currentPhaseIndex].currentTime < 0) {
            //TODO Lose Game
        }

        if (nRecepiesArrived < myPhases[currentPhaseIndex].totalRecepiesInThisPhase) {

            partialTime += Time.deltaTime;
            if(partialTime > myPhases[currentPhaseIndex].timeToAddRecepie || activeRecepies.Count <= 1) {
                //Add recepie
                partialTime = 0;
                AddNewRecepie();
            }
        }



        foreach (Recepie recepie in activeRecepies) {
            recepie.currentTime -= Time.deltaTime;
            if(recepie.currentTime <= 0) {
                didntFinishInTimeRecepie.Invoke();
                recepiesToRemove.Add(recepie);
            }
        }
        if(recepiesToRemove.Count > 0) {
            activeRecepies.RemoveAll(i => recepiesToRemove.Contains(i));
            recepiesToRemove.Clear();

        }
    }


    public void DeliverARecepie(Recepie recepie) {
        if (recepie == null) { return; }
        if(activeRecepies.Contains(recepie)) {
            activeRecepies.Remove(recepie);
            myPhases[currentPhaseIndex].currentTime += recepie.timeBonusOnComplete * recepie.currentTime / recepie.timeToFinishRecepie;
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
            Recepie newRecepie = currentPhase.possibleRecepies[Random.Range(0, currentPhase.possibleRecepies.Length)];
            activeRecepies.Add(newRecepie);
            nRecepiesArrived++;
            newRecepie.currentTime = newRecepie.timeToFinishRecepie;
        }
        else {
            //TODO finishGame
        }
        newRecepieEntering.Invoke();
    }
}
