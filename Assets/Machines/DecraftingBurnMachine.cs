using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecraftingBurnMachine : DecraftingMachine
{
    [SerializeField] float burnTime;
    [SerializeField] Transform spawnPoint;
    float currentBurnTime;
    public bool isBurning = false;
    private ItemComponent craftedItem;
    private Color green = Color.green;
    private Color yellow = Color.yellow;
    private Color red = Color.red;
    protected override void Awake() {
        base.Awake();
    }
    protected override void Update() {
        //if (!placedItems[0]) return;
        if (isStarted) {
            if (hasProgressBar)
            {
                progressBar.gameObject.SetActive(true);
                progressBar.Progress = 1f - currentDecraftingTime / decraftingTime;
            }
            currentDecraftingTime -= Time.deltaTime;
            if (!placedItems[0]) {
                isStarted = false;
                currentDecraftingTime = decraftingTime;
                if (hasProgressBar)
                {
                    progressBar.gameObject.SetActive(false);
                }
            }
            if (currentDecraftingTime <= 0) {
                isStarted = false;
                recipeManager.NewRecipe(placedItems[0].ingredientScriptable, workstationType);
                placedItems[0].gameObject.SetActive(false);
                placedItems[0].transform.parent = ItemsObjectPool.Instance.transform;
                placedItems[0] = null;
                isBurning = true;
                currentBurnTime = burnTime;
                
                SpawnMaterial();
            }
        }
        if (isBurning) {

            if (hasProgressBar)
            {
                float percent = Mathf.Clamp01(currentBurnTime / burnTime);

                if (percent >= 0.5f)
                {
                    progressBar.BarImage.color = Color.Lerp(yellow, green, percent * 2 - 1);
                }
                else
                    progressBar.BarImage.color = Color.Lerp(red, yellow, percent * 2);
            }

            currentBurnTime -= Time.deltaTime;
            if (currentBurnTime <= 0) {
                craftedItem.gameObject.SetActive(false);
                craftedItem.transform.parent = ItemsObjectPool.Instance.transform;

                if (hasProgressBar)
                {
                    progressBar.gameObject.SetActive(false);
                }

                craftedItem = null;
                isBurning = false;
                currentBurnTime = burnTime;
            }
        }
        animator.SetBool("IsActive", isStarted || isBurning);
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

        if (hasProgressBar)
        {
            progressBar.gameObject.SetActive(false);
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
        currentDecraftingTime = decraftingTime;
        craftedItem = ItemsObjectPool.Instance.GetItem(materialDrop.ingredientScriptable);
        craftedItem.transform.position = spawnPoint.position;
        craftedItem.GetComponent<Rigidbody>().useGravity = false;
        finished.Invoke();
    }
}
