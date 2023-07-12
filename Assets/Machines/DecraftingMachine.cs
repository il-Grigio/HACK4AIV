using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecraftingMachine : MachineScript
{
    [SerializeField] private float decraftingTime;
    [SerializeField] private ItemComponent[] compatibleItems;
    [SerializeField] private ItemComponent materialDrop;
    [SerializeField] private float currentDecraftingTime;
    private bool isStarted;

    private void Update() {
        if (isStarted) {
            currentDecraftingTime -= Time.deltaTime;
            if(currentDecraftingTime <= 0) {
                isStarted = false;
                SpawnMaterial();
                placedItems[0] = null;
            }
        }
    }
    public override bool Interact() {
        if(placedItems.Length > 0) {
            if (placedItems[0] && placedItems[0].ingredientScriptable.ingredients.Length > 0) {
                currentDecraftingTime = decraftingTime;
                isStarted = true;
                return true;
            }
        }
        return false;
    }

    //Spawns material with a small hop
    private void SpawnMaterial() {
        ItemComponent item = Instantiate<ItemComponent>(materialDrop);
        item.transform.position = placeItemPositions[0].position;
        //Rigidbody itemrb = materialDrop.GetComponent<Rigidbody>();
        //itemrb.AddForce
    }

#if UNITY_EDITOR
    private void OnGUI() {
        if(GUI.Button(new Rect(10, 10, 60, 30), "Interact")) {
            Interact();
        }
    }
#endif
}
