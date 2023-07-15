using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
                ClientOrderMGR.Instance.DeliverAnItem(myItem.ingredientScriptable);
                RemoveItem(myItem);
                //TODO
                myItem.gameObject.SetActive(false);
                finished.Invoke();
            }
        }
        else {
            currentTime = 0;
        }
    }
}
