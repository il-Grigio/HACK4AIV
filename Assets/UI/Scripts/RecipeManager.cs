using Grigios;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum Workstation
{
    MetalStation,
    WoodStation,
    PlasticStation,
    RubberStation,
    GlassStation,
    ClothStation,
    CircuitsStation,
    CraftingStation
}

public class RecipeManager : Singleton<RecipeManager>
{
    [SerializeField]
    private RectTransformTest recipeContainer;
    [SerializeField]
    private UIUnitManager UIPrefab;

    [SerializeField] private IngredientScriptable metal;
    [SerializeField] private IngredientScriptable wood;
    [SerializeField] private IngredientScriptable plastic;
    [SerializeField] private IngredientScriptable rubber;
    [SerializeField] private IngredientScriptable glass;
    [SerializeField] private IngredientScriptable cloth;
    [SerializeField] private IngredientScriptable circuits;
    [Space]
    [SerializeField] private Sprite metalBench;
    [SerializeField] private Sprite woodBench;
    [SerializeField] private Sprite plasticBench;
    [SerializeField] private Sprite rubberBench;
    [SerializeField] private Sprite glassBench;
    [SerializeField] private Sprite clothBench;
    [SerializeField] private Sprite circuitsBench;


    private List<UIIngredient> items;
    [HideInInspector] public Dictionary<Workstation, IngredientScriptable> materialIcons;
    [HideInInspector] public Dictionary<Workstation, Sprite> benchSprites;

    // Start is called before the first frame update
    void Awake()
    {
        items = new List<UIIngredient>();
        materialIcons = new Dictionary<Workstation, IngredientScriptable>();
        benchSprites = new Dictionary<Workstation, Sprite>();

        materialIcons.Add(Workstation.MetalStation, metal);
        materialIcons.Add(Workstation.WoodStation, wood);
        materialIcons.Add(Workstation.PlasticStation, plastic);
        materialIcons.Add(Workstation.RubberStation, rubber);
        materialIcons.Add(Workstation.GlassStation, glass);
        materialIcons.Add(Workstation.ClothStation, cloth);
        materialIcons.Add(Workstation.CircuitsStation, circuits);

        benchSprites.Add(Workstation.MetalStation, metalBench);
        benchSprites.Add(Workstation.WoodStation, woodBench);
        benchSprites.Add(Workstation.PlasticStation, plasticBench);
        benchSprites.Add(Workstation.RubberStation, rubberBench);
        benchSprites.Add(Workstation.GlassStation, glassBench);
        benchSprites.Add(Workstation.ClothStation, clothBench);
        benchSprites.Add(Workstation.CircuitsStation, circuitsBench);

    }

    private void AddItemToUI(UIIngredient item)
    {
        UIUnitManager newUI = GameObject.Instantiate(UIPrefab, recipeContainer.transform);
        newUI.ingredient = item.ingredient;
        newUI.ingredientImage.sprite = item.ingredient.icon;
        newUI.benchImage.sprite = benchSprites[item.station];

        recipeContainer.UpdateContainerSize();
    }

    private bool ContainsAttribute()
    {
        return true;
    }

    public void NewRecipe(IngredientScriptable newItem, Workstation station)
    {
        UIIngredient newIngredient = new UIIngredient(newItem, station);
        if (!CheckForDuplicates(newIngredient))
        {
            items.Add(newIngredient);
        }
        else
        {
            return;
        }
        
        AddItemToUI(newIngredient);
    }

    private bool CheckForDuplicates(UIIngredient ingredientToCheck)
    {
        foreach (var item in items)
        {
            if (item.ingredient == ingredientToCheck.ingredient && item.station == ingredientToCheck.station)
            {
                return true;
            }
        }

        return false;
    }
}


public class UIIngredient
{
    public IngredientScriptable ingredient;
    public Workstation station;

    public UIIngredient() { }

    public UIIngredient(IngredientScriptable _ingredient, Workstation _station)
    {
        ingredient = _ingredient;
        station = _station;
    }
}
