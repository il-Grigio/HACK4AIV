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

    [SerializeField] private IngredientScriptable bench_metal;
    [SerializeField] private IngredientScriptable bench_wood;
    [SerializeField] private IngredientScriptable bench_plastic;
    [SerializeField] private IngredientScriptable bench_rubber;
    [SerializeField] private IngredientScriptable bench_glass;
    [SerializeField] private IngredientScriptable bench_cloth;
    [SerializeField] private IngredientScriptable bench_circuits;

    private List<UIIngredient> items;
    [HideInInspector] public Dictionary<Workstation, IngredientScriptable> materialIcons;

    // Start is called before the first frame update
    void Awake()
    {
        items = new List<UIIngredient>();
        materialIcons = new Dictionary<Workstation, IngredientScriptable>();

        materialIcons.Add(Workstation.MetalStation, bench_metal);
        materialIcons.Add(Workstation.WoodStation, bench_wood);
        materialIcons.Add(Workstation.PlasticStation, bench_plastic);
        materialIcons.Add(Workstation.RubberStation, bench_rubber);
        materialIcons.Add(Workstation.GlassStation, bench_glass);
        materialIcons.Add(Workstation.ClothStation, bench_cloth);
        materialIcons.Add(Workstation.CircuitsStation, bench_circuits);

    }

    private void AddItemToUI(UIIngredient item)
    {
        UIUnitManager newUI = GameObject.Instantiate(UIPrefab, recipeContainer.transform);
        newUI.ingredient = item.ingredient;
        newUI.ingredientImage.sprite = item.ingredient.icon;
        newUI.benchImage.sprite = materialIcons[item.station].icon;

        recipeContainer.UpdateContainerSize();
    }

    private bool ContainsAttribute()
    {
        return true;
    }

    public void NewRecipe(IngredientScriptable newItem, Workstation station)
    {
        UIIngredient newIngredient = new UIIngredient(newItem, station);
        if (!items.Contains(newIngredient))
        {
            items.Add(newIngredient);
        }
        else
        {
            return;
        }
            
        AddItemToUI(newIngredient);
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
