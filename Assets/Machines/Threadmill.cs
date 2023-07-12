using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threadmill : MachineScript {
    [SerializeField] Transform finalDestination;
    [SerializeField] float timeToMove = 5;
    float currentTime;
    public override bool Interact() {
        return true;
    }
    private void Update() {
        if (placedItems[0]) {
            currentTime += Time.deltaTime;
            placedItems[0].transform.position = Vector3.Lerp(placeItemPositions[0].position, finalDestination.position, currentTime / timeToMove);
            if(currentTime / timeToMove >= 1) {
                ItemComponent myItem = placedItems[0];
                RemoveItem(myItem);
                //TODO
                myItem.gameObject.SetActive(false);
            }
        }
        else {
            currentTime = 0;
        }
    }
}
