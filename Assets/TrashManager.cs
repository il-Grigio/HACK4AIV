using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrashManager : Grigios.Singleton<TrashManager> {
    [System.Serializable]
    class TrashPhase {
        public ItemComponent[] possibleTrash;
        public int maxNumberOfTrashInScene;

        public float minDelayTime = 1;
        public float maxDelayTime = 30;
    }

    [SerializeField] TrashPhase[] trashPhases;
    [SerializeField] int maxNumberOfItemsInScene;
    [SerializeField] EnteringThreadmill enteringThreadmill;
    //varie fasi

    bool isSpawning;
    float currentSpawningTime;
    ClientOrderMGR clientOrderMGR;
    ItemComponent[] activeItems = new ItemComponent[0];
    int currentTrash;


    ItemComponent currentItemToSpawn;

    private void Awake() {
        clientOrderMGR = ClientOrderMGR.Instance;
    }

    public void SetActiveItems() {
        currentTrash = 0;
        activeItems = FindObjectsByType<ItemComponent>(FindObjectsSortMode.None);
        if (clientOrderMGR.CurrentPhaseIndex >= clientOrderMGR.NPhases) return;
        for (int i = 0; i < activeItems.Length; i++) {
            foreach (ItemComponent possibleTrash in trashPhases[clientOrderMGR.CurrentPhaseIndex].possibleTrash) {
                if (possibleTrash.ingredientScriptable == activeItems[i].ingredientScriptable) 
                    currentTrash++;
            }

            //trashPhases[clientOrderMGR.CurrentPhaseIndex].possibleTrash.ToList().Contains(activeItems[i].ingredientScriptable);

            //if (System.Array.Exists(trashPhases[clientOrderMGR.CurrentPhaseIndex].possibleTrash, obj => obj == activeItems[i].ingredientScriptable)){
            //    currentTrash++;
            //}
        }
    }

    private void Update() {
        if(!isSpawning && activeItems.Length < maxNumberOfItemsInScene && currentTrash < trashPhases[clientOrderMGR.CurrentPhaseIndex].maxNumberOfTrashInScene) {
            StartSpawning();
        }
        if (isSpawning) {
            currentSpawningTime -= Time.deltaTime;
            if(currentSpawningTime <= 0) {
                isSpawning = false;
                SpawnNow();
            }
        }
    }

    private void StartSpawning() {
        isSpawning = true;
        ItemComponent[] possibleTrash = trashPhases[clientOrderMGR.CurrentPhaseIndex].possibleTrash;
        currentItemToSpawn = possibleTrash[Random.Range(0, possibleTrash.Length)];
        currentSpawningTime = Random.Range(trashPhases[clientOrderMGR.CurrentPhaseIndex].minDelayTime, trashPhases[clientOrderMGR.CurrentPhaseIndex].maxDelayTime);
    }
    public void SpawnNow() {
        if (!currentItemToSpawn) {
            ItemComponent[] possibleTrash = trashPhases[ClientOrderMGR.Instance.CurrentPhaseIndex].possibleTrash;
            currentItemToSpawn = possibleTrash[Random.Range(0, possibleTrash.Length)];
        }
        ItemComponent spawnedItem = ItemsObjectPool.Instance.GetItem(currentItemToSpawn.ingredientScriptable);
        enteringThreadmill.PlaceItem(spawnedItem);
        SetActiveItems();
    }
}
