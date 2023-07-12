using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecraftingMachine : MachineScript
{
    [SerializeField] private float decraftingTime;
    private float currentDecraftingTime;
    private bool isStarted;

    private void Update() {
        if (isStarted) {

        }
    }
    public override bool Interact() {
        if(placedItems.Length > 0) {
            if (placedItems[0].ingredientScriptable.ingredients.Length > 0) {
                currentDecraftingTime = decraftingTime;
                isStarted = true;
                return true;
            }
        }
        return false;
    }
}
