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
    [SerializeField] private RectTransform timeRect;

    private List<UIIngredientManager> ingredients;
    [HideInInspector] public float MaxTime;
    [HideInInspector] public float currentTime;
    //when time = 0, posX is -380

    public UICraftingUnit() { }

    public UICraftingUnit(IngredientScriptable _ingredient)
    {
        ingredient = _ingredient;
    }

    private void Start()
    {
        ingredientImage.sprite = ingredient.icon;
        ingredients = new List<UIIngredientManager>();
        foreach(var outputIngredient in ingredient.ingredients)
        {
            UIIngredientManager newIgredient = GameObject.Instantiate(ingredientSample, ingredientsContainer.transform);
            newIgredient.outputIngredientIcon.sprite = outputIngredient.icon;
        }
        timeRect.GetComponent<Animator>().SetFloat("TimeMultiplier", 1f / MaxTime);
        currentTime = 0;
    }

    public void RemoveItem()
    {
        Destroy(gameObject);
    }
}
