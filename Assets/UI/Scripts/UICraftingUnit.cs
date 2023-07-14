using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftingUnit : MonoBehaviour
{
    [SerializeField] public IngredientScriptable ingredient;
    [SerializeField] public Image ingredientImage;
    [SerializeField] public RectTransform ingredientsContainer;
    [SerializeField] private UIIngredientManager ingredientSample;

    private List<UIIngredientManager> ingredients;
    [HideInInspector] public float MaxTime;
    [HideInInspector] public float currentTime;

    public UICraftingUnit() { }

    public UICraftingUnit(IngredientScriptable _ingredient)
    {
        ingredient = _ingredient;
    }

    private void Awake()
    {
        ingredientImage.sprite = ingredient.icon;
        ingredients = new List<UIIngredientManager>();
        foreach(var outputIngredient in ingredient.ingredients)
        {
            UIIngredientManager newIgredient = GameObject.Instantiate(ingredientSample, ingredientsContainer.transform);
            ingredientSample.outputIngredientIcon.sprite = outputIngredient.icon;
        }
    }

    private void Update()
    {
        
    }
}
