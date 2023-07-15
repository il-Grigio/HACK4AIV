using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecraftingBurnMachine : DecraftingMachine
{
    [SerializeField] float burnTime;
    [SerializeField] Transform spawnPoint;
    float currentBurnTime;
    bool isBurning = false;
    private ItemComponent craftedItem;
    protected override void Update() {
        if (!placedItems[0]) return;
        if (isStarted) {
            currentDecraftingTime -= Time.deltaTime;
            if (currentDecraftingTime <= 0) {
                isStarted = false;
                recipeManager.NewRecipe(placedItems[0].ingredientScriptable, workstationType);
                placedItems[0].gameObject.SetActive(false);
                placedItems[0].transform.parent = ItemsObjectPool.Instance.transform;
                isBurning = true;
                currentBurnTime = burnTime;
                SpawnMaterial();
            }
        }
        if (isBurning) {
            currentBurnTime -= Time.deltaTime;
            if (currentBurnTime <= 0) {
                craftedItem.gameObject.SetActive(false);
                placedItems[0].transform.parent = ItemsObjectPool.Instance.transform;

                craftedItem = null;
                isBurning = false;
                currentBurnTime = burnTime;
            }
        }
    }
    public override ItemComponent GetFirstItem() {
        if (craftedItem) {
            return craftedItem;
        }
        return base.GetFirstItem();
    }

    public override void RemoveItem(ItemComponent item) {
        if (craftedItem == item) {
            isBurning = false;
            craftedItem = null;
        }
        else {
            for (int i = 0; i < placedItems.Length; i++) {
                if (placedItems[i] == item) {
                    placedItems[i] = null;
                    return;
                }
            }
        }
    }

    protected override void SpawnMaterial() {
        craftedItem = ItemsObjectPool.Instance.GetItem(materialDrop.ingredientScriptable);
        craftedItem.transform.position = spawnPoint.position;
        craftedItem.GetComponent<Rigidbody>().useGravity = false;
    }
}
