using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MachineType { 
    Decrafting,
    DecraftingSpam,
    Crafting
}
public abstract class MachineScript : MonoBehaviour
{
    [SerializeField] protected MachineType MachineType;
    [SerializeField] protected Transform[] placeItemPositions;
    protected ItemComponent[] placedItems;
    private void Awake() {
        placedItems = new ItemComponent[placeItemPositions.Length];
    }
    public void PlaceItem(ItemComponent item) {
        for (int i = 0; i < placeItemPositions.Length; i++) {
            if (placedItems[i] == null) {
                placedItems[i] = item;
                placedItems[i].transform.parent = transform;
                placedItems[i].transform.position = placeItemPositions[i].position;
                return;
            }
        }
    }
    public ItemComponent GetFirstItem() {
        for (int i = 0;i < placedItems.Length;i++) { 
            if (placedItems[i]) {
                return placedItems[i];
            } 
        }
        return null;
    }
    public void RemoveItem(ItemComponent item) {
        for (int i = 0; i < placedItems.Length; i++) {
            if (placedItems[i] == item) {
                placedItems[i] = null;
                return;
            }
        }
    }

    public bool CanPlaceItems() {
        for (int i = 0; i < placeItemPositions.Length; i++) {
            if (placedItems[i] == null) return true;
        }
        return false;
    }

    public abstract bool Interact();
}
