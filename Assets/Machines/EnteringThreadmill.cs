using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteringThreadmill : MachineScript {
    [SerializeField] Transform finalDestination;
    [SerializeField] float timeToMove = 5;
    float currentTime;
    Animator animator;
    protected override void Awake() {
        base.Awake();
        animator= GetComponent<Animator>();
    }
    public override bool CanPlaceItems() {
        return false;
    }
    public override ItemComponent GetFirstItem() {
        return null;
    }
    public override bool Interact() {
        return false;
    }

    private void Update() {
        if (animator) animator.SetBool("IsActive", placedItems[0]);
        if (placedItems[0]) {
            currentTime += Time.deltaTime;
            placedItems[0].transform.position = Vector3.Lerp(placeItemPositions[0].position, finalDestination.position, currentTime / timeToMove);
            if (currentTime / timeToMove >= 1) {
                ItemComponent myItem = placedItems[0];
                RemoveItem(myItem);
                myItem.transform.parent = null;
                myItem.transform.GetComponent<Rigidbody>().useGravity = true;
                myItem.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                myItem.transform.GetComponent<Collider>().enabled = true;
            }
        }
        else {
            currentTime = 0;
        }
    }
}
