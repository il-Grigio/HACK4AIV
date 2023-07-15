using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DecraftingMachine : MachineScript
{
    [SerializeField] protected Workstation workstationType;
    [SerializeField] private float decraftingTime;
    [SerializeField] protected ItemComponent materialDrop;
    protected float currentDecraftingTime;
    [HideInInspector]public bool isStarted;
    private float itemRotationSpeed;
    [SerializeField] private float itemThrowForce;

    [SerializeField] protected UnityEvent cantInteract;

    protected RecipeManager recipeManager;

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
                placedItems[0].gameObject.SetActive(false);
                placedItems[0].transform.parent = ItemsObjectPool.Instance.transform;
                placedItems[0] = null;
            }
        }
    }

    //When the player interacts with the machine, use this function (Es. start crafting)
    public override bool Interact() {
        if (isStarted) {
            cantInteract.Invoke();
            return false;
        }
        if (placedItems[0] && placedItems[0].ingredientScriptable.ingredients.Length > 0) {
            if (CheckCorrectMaterial()) {
                currentDecraftingTime = decraftingTime;
                isStarted = true;
                return true;
            }
        }
        cantInteract.Invoke();
        return false;
    }

    //Spawns material with a small hop
    protected virtual void SpawnMaterial() {
        ItemComponent item = ItemsObjectPool.Instance.GetItem(materialDrop.ingredientScriptable);
        //item.ingredientScriptable = RecipeManager.Instance.materialIcons[workstationType];
        item.transform.position = placeItemPositions[0].position;
        item.GetComponent<Collider>().enabled = true;
        Rigidbody itemrb = item.GetComponent<Rigidbody>();
        itemrb.useGravity = true;
        itemrb.AddForce(-transform.forward * itemThrowForce);
        finished.Invoke();
    }

    protected bool CheckCorrectMaterial() {
        if (placedItems[0] == null) return false;
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
