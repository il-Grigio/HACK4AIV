using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecraftingSpamMachine : DecraftingMachine
{
    [SerializeField] private int interactionsTimes;
    int currentInteractions;
    public override bool Interact() {
        if (!CheckCorrectMaterial()) return false;
        if(currentInteractions < interactionsTimes) {
            currentInteractions++;
            if(currentInteractions ==  interactionsTimes) {
                SpawnMaterial();
                recipeManager.NewRecipe(placedItems[0].ingredientScriptable, workstationType);
                Destroy(placedItems[0].gameObject);
                placedItems[0] = null;
                currentInteractions = 0;
            }
        }
        return true;
    }
    protected override void Update() {
        return;
    }
}
