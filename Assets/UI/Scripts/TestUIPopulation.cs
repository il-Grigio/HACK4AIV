using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIPopulation : MonoBehaviour
{
    [SerializeField] private IngredientScriptable ingredient;
    [SerializeField] private Workstation station;
    [Space]
    [SerializeField] private bool spawnIcon = false;

    private RecipeManager recipeManager;

    private void Awake()
    {
        recipeManager = GetComponent<RecipeManager>();
    }

    private void Update()
    {
        if (spawnIcon)
        {
            spawnIcon = false;
            recipeManager.NewRecipe(ingredient, station);
        }
    }
}
