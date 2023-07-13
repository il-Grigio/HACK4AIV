using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMachine : MachineScript
{

    [SerializeField] private CraftingStand[] craftingStands;
    [SerializeField] private IngredientScriptable[] craftables;
    private Dictionary<string, IngredientScriptable> recipes;
    [SerializeField] private Transform spawnPoint;

    public override bool Interact() {
        string combination = "";
        foreach(CraftingStand stand in craftingStands) {
            if (stand.getPlacedItem()) {
                combination += stand.getPlacedItem().ingredientScriptable.fullName;
            }
        }

        if (recipes.ContainsKey(combination)) {
            Debug.Log("Item crafted: " + recipes[combination]);
            return true;
        }
        Debug.Log("Combination is invalid");
        return false;
    }

    private void Awake() {
        //Fill Dictionary with recipes
        recipes = new Dictionary<string, IngredientScriptable>();
        for (int i = 0; i < craftables.Length; i++) {
            string key = "";
            foreach(IngredientScriptable ingredient in craftables[i].ingredients) {
                key += ingredient.fullName;
            }
            Debug.Log(key);
            recipes.Add(key, craftables[i]);
        }
    }

#if UNITY_EDITOR
    private void OnGUI() {
        if (GUI.Button(new Rect(10, 40, 60, 30), "Craft")) {
            Interact();
        }
    }
#endif
}
