using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecraftingSpamMachine : DecraftingMachine
{
    [SerializeField] private int interactionsTimes;
    public int currentInteractions;

    public override bool Interact() {
        if (!CheckCorrectMaterial()) {
            cantInteract.Invoke();
            return false;
        }
        if(currentInteractions < interactionsTimes) {
            currentInteractions++;
            if (hasProgressBar)
            {
                progressBar.gameObject.SetActive(true);
                progressBar.Progress = (1f / interactionsTimes) * currentInteractions;
            }
            if(currentInteractions ==  interactionsTimes) {
                SpawnMaterial();
                recipeManager.NewRecipe(placedItems[0].ingredientScriptable, workstationType);
                placedItems[0].gameObject.SetActive(false);
                placedItems[0].transform.parent = ItemsObjectPool.Instance.transform;
                placedItems[0] = null;
                currentInteractions = 0;
                if (hasProgressBar)
                {
                    progressBar.gameObject.SetActive(false);
                }
            }
        }
        animator.SetTrigger("IsActive");
        return true;
    }
    public override void RemoveItem(ItemComponent item) {
        base.RemoveItem(item);
        currentInteractions = 0;
        progressBar.gameObject.SetActive(false);
    }
    protected override void Update() {
        return;
    }
}
