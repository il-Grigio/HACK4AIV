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
    ItemComponent[] activeItems;
    int currentTrash;


    ItemComponent currentItemToSpawn;

    private void Start() {
        clientOrderMGR = ClientOrderMGR.Instance;
    }

    public void SetActiveItems() {
        currentTrash = 0;
        activeItems = FindObjectsByType<ItemComponent>(FindObjectsSortMode.None);

        for (int i = 0; i < activeItems.Length; i++) {
            if (System.Array.Exists(trashPhases[clientOrderMGR.CurrentPhaseIndex].possibleTrash, obj => obj == activeItems[i].ingredientScriptable)){
                currentTrash++;
            }
        }
    }

    private void Update() {
        if(!isSpawning && activeItems.Length < maxNumberOfItemsInScene && currentTrash < trashPhases[clientOrderMGR.CurrentPhaseIndex].maxNumberOfTrashInScene) {
            StartSpawning();
        }
        if (isSpawning) {
            currentSpawningTime -= Time.time;
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
    private void SpawnNow() {
        ItemComponent spawnedItem = Instantiate(currentItemToSpawn);
        enteringThreadmill.PlaceItem(spawnedItem);
    }
}