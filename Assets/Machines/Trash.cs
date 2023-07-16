using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MachineScript
{
    public override void PlaceItem(ItemComponent item) {
        for (int i = 0; i < placeItemPositions.Length; i++) {
            if (placedItems[i] == null) {
                placedItems[i] = item;
                placedItems[i].transform.position = placeItemPositions[i].position;
                placedItems[i].gameObject.SetActive(false);
                placedItems[i].transform.parent = ItemsObjectPool.Instance.transform; 
                placedItems[i] = null;
                return;
            }
        }
    }
    public override bool Interact() {
        return false;
    }
}
