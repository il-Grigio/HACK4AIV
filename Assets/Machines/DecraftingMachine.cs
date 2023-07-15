using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DecraftingMachine : MachineScript
{
    [SerializeField] Workstation workstationType;
    [SerializeField] private float decraftingTime;
    [SerializeField] protected ItemComponent materialDrop;
    protected float currentDecraftingTime;
    [HideInInspector]public bool isStarted;
    private float itemRotationSpeed;
    [SerializeField] private float itemThrowForce;

    private RecipeManager recipeManager;

    private void Start()
    {
        recipeManager = GameObject.Find("RecipesMenu").GetComponent<RecipeManager>();
    }

    protected virtual void Update() {
        if (isStarted) {
            currentDecraftingTime -= Time.deltaTime;
            if(currentDecraftingTime <= 0) {
                isStarted = false;
                SpawnMaterial();
                recipeManager.NewRecipe(placedItems[0].ingredientScriptable, workstationType);
                Destroy(placedItems[0].gameObject);
                placedItems[0] = null;
            }
        }
    }

    //When the player interacts with the machine, use this function (Es. start crafting)
    public override bool Interact() {
        if (isStarted) return false;
        if (placedItems[0] && placedItems[0].ingredientScriptable.ingredients.Length > 0) {
            if (CheckCorrectMaterial()) {
                currentDecraftingTime = decraftingTime;
                isStarted = true;
                return true;
            }
        }
        return false;
    }

    //Spawns material with a small hop
    protected virtual void SpawnMaterial() {
        ItemComponent item = Instantiate<ItemComponent>(materialDrop);
        //item.ingredientScriptable = RecipeManager.Instance.materialIcons[workstationType];
        item.transform.position = placeItemPositions[0].position;
        Rigidbody itemrb = item.GetComponent<Rigidbody>();
        itemrb.AddForce(-transform.forward * itemThrowForce);
    }

    protected bool CheckCorrectMaterial() {
        return placedItems[0].ingredientScriptable.ingredients.Contains(RecipeManager.Instance.materialIcons[workstationType]);
        //foreach(IngredientScriptable item in placedItems[0].ingredientScriptable.ingredients) {
        //    //if(string.Compare(item.fullName, materialDrop.ingredientScriptable.fullName) == 0) {
        //    //if () { 
        //    //    Debug.Log("Material found");
        //    //    return true;
        //    //}
        //}
        //Debug.Log("Material not found");
        //return false;
    }

#if UNITY_EDITOR
    private void OnGUI() {
        if(GUI.Button(new Rect(10, 10, 60, 30), "Interact")) {
            Interact();
        }
    }
#endif
}
