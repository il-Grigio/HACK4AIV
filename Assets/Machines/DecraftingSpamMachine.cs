using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecraftingSpamMachine : DecraftingMachine
{
    [SerializeField] private int interactionsTimes;
    public int currentInteractions;
    public override bool Interact() {
        if (!CheckCorrectMaterial()) return false;
        if(currentInteractions < interactionsTimes) {
            currentInteractions++;
            if(currentInteractions ==  interactionsTimes) {
                SpawnMaterial();
                recipeManager.NewRecipe(placedItems[0].ingredientScriptable, workstationType);
                placedItems[0].gameObject.SetActive(false);
                placedItems[0].transform.parent = ItemsObjectPool.Instance.transform;
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
