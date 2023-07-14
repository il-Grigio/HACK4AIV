using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MachineType { 
    Decrafting,
    DecraftingSpam,
    Crafting,
    Table,
    Threadmill
}
public abstract class MachineScript : MonoBehaviour
{
    Workstation workstation;
    //[SerializeField] protected MachineType MachineType;
    [SerializeField] protected Transform[] placeItemPositions;
    [SerializeField] protected ItemComponent[] placedItems;
    private void Awake() {
        placedItems = new ItemComponent[placeItemPositions.Length];
    }
    public virtual void PlaceItem(ItemComponent item) {
        for (int i = 0; i < placeItemPositions.Length; i++) {
            if (placedItems[i] == null) {
                placedItems[i] = item;
                placedItems[i].transform.parent = transform;
                placedItems[i].transform.position = placeItemPositions[i].position;
                return;
            }
        }
    }
    public virtual ItemComponent GetFirstItem() {
        for (int i = 0;i < placedItems.Length;i++) { 
            if (placedItems[i]) {
                return placedItems[i];
            } 
        }
        return null;
    }
    public virtual void RemoveItem(ItemComponent item) {
        for (int i = 0; i < placedItems.Length; i++) {
            if (placedItems[i] == item) {
                placedItems[i] = null;
                return;
            }
        }
    }

    public virtual bool CanPlaceItems() {
        for (int i = 0; i < placeItemPositions.Length; i++) {
            if (placedItems[i] == null) return true;
        }
        return false;
    }

    public abstract bool Interact();
}
