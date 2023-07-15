using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CraftingMachine : MachineScript
{

    [SerializeField] private IngredientScriptable[] craftables;
    [SerializeField] private IngredientScriptable genericCraftable;
    [SerializeField] private ItemComponent blankItem;
    [SerializeField] float craftingTime = 5;
    float currentCraftingTime = 0;
    //private Dictionary<string, IngredientScriptable> recipes;
    [SerializeField] private Transform spawnPoint;


    public IngredientScriptable itemToCraft;
    private ItemComponent craftedItem;

    public override bool Interact() {
        bool areEqual = false;
        List<IngredientScriptable> list2 = new List<IngredientScriptable> ();
        for (int i = 0; i < placedItems.Length; i++) {
            //item not exist
            if (placedItems[i] == null) continue;
            //toys or base materials
            else if (craftables.Contains(placedItems[i].ingredientScriptable) || placedItems[i].ingredientScriptable == genericCraftable) 
                list2.AddRange(placedItems[i].ingredientScriptable.ingredients);
            //trash
            else list2.Add(placedItems[i].ingredientScriptable);
        }


        foreach (IngredientScriptable craftable in craftables) {
            List<IngredientScriptable> list1 = craftable.ingredients.ToList();

            areEqual = list1.Count == list2.Count && list1.All(x => list2.Contains(x) && list1.Count(y => y == x) == list2.Count(y => y == x));
            if (areEqual) {
                itemToCraft = craftable;
                break;
            }
        }
        if (!areEqual) {
            itemToCraft = genericCraftable;
            itemToCraft.ingredients = list2.ToArray();
        }
        

        //foreach(CraftingStand stand in craftingStands) {
        //    if (stand.getPlacedItem()) {
        //        combination += stand.getPlacedItem().ingredientScriptable.fullName;
        //    }
        //}

        //if (recipes.ContainsKey(combination)) {
        //    Debug.Log("Item crafted: " + recipes[combination]);
        //    return true;
        //}
        //Debug.Log("Combination is invalid");
        return false;
    }
    private void Update() {
        if(itemToCraft) {
            currentCraftingTime += Time.deltaTime;
        }
        else {
            currentCraftingTime -= Time.deltaTime;
        }
        currentCraftingTime = Mathf.Clamp(currentCraftingTime, 0, craftingTime);
        for (int i = 0; i < placedItems.Length; i++) {
            if(placedItems[i] == null) continue;
            placedItems[i].transform.position = Vector3.Lerp(placeItemPositions[i].position, spawnPoint.position, currentCraftingTime / craftingTime);
        }
        if(currentCraftingTime == craftingTime) {
            CraftNow();
        }
    }

    private void CraftNow() {
        currentCraftingTime = 0;
        for (int i = 0; i < placedItems.Length; i++) {
            if (placedItems[i] == null) continue;

            placedItems[i].gameObject.SetActive(false);
            placedItems[i] = null;
        }
        craftedItem = ItemsObjectPool.Instance.GetItem(itemToCraft);
        craftedItem.GetComponent<Collider>().enabled = false;
        craftedItem.GetComponent<Rigidbody>().useGravity = false;
        craftedItem.transform.position = spawnPoint.position;
        itemToCraft = null;
        finished.Invoke();
    }
    public override void PlaceItem(ItemComponent item) {
        itemToCraft = null;
        base.PlaceItem(item);
    }
    public override bool CanPlaceItems() {
        if (!machineActive) return false;
        int positionsOccuipied = 0;
        for (int i = 0; i < placedItems.Length; i++) {
            if (placedItems[i] == null) continue;
            if (placedItems[i].ingredientScriptable.ingredients.Length > 0) {
                positionsOccuipied += placedItems[i].ingredientScriptable.ingredients.Length;
            }
            else {
                positionsOccuipied++;
            }
        }
        return positionsOccuipied < placedItems.Length;
    }

    public override ItemComponent GetFirstItem() {
        if (craftedItem) {
            return craftedItem;
        }
        return base.GetFirstItem();
    }
    public override void RemoveItem(ItemComponent item) {
        if (craftedItem == item) craftedItem = null;
        else {
            for (int i = 0; i < placedItems.Length; i++) {
                if (placedItems[i] == item) {
                    placedItems[i] = null;
                    itemToCraft = null;
                    return;
                }
            }
        }
    }

    //private void Awake() {
        ////Fill Dictionary with recipes
        //recipes = new Dictionary<string, IngredientScriptable>();
        //for (int i = 0; i < craftables.Length; i++) {
        //    string key = "";
        //    foreach(IngredientScriptable ingredient in craftables[i].ingredients) {
        //        key += ingredient.fullName;
        //    }
        //    Debug.Log(key);
        //    recipes.Add(key, craftables[i]);
        //}
   // }

#if UNITY_EDITOR
    private void OnGUI() {
        if (GUI.Button(new Rect(10, 40, 60, 30), "Craft")) {
            Interact();
        }
    }
#endif
}
