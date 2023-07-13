using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using Unity.VisualScripting;
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

public class RecipeManager : MonoBehaviour
{
    [SerializeField]
    private RectTransformTest recipeContainer;
    [SerializeField]
    private UIUnitManager UIPrefab;

    [SerializeField] private Sprite bench_metal_image;
    [SerializeField] private Sprite bench_wood_image;
    [SerializeField] private Sprite bench_plastic_image;
    [SerializeField] private Sprite bench_rubber_image;
    [SerializeField] private Sprite bench_glass_image;
    [SerializeField] private Sprite bench_cloth_image;
    [SerializeField] private Sprite bench_circuits_image;

    private List<UIIngredient> items;
    private Dictionary<Workstation, Sprite> materialIcons;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<UIIngredient>();
        materialIcons = new Dictionary<Workstation, Sprite>();

        materialIcons.Add(Workstation.MetalStation, bench_metal_image);
        materialIcons.Add(Workstation.WoodStation, bench_wood_image);
        materialIcons.Add(Workstation.PlasticStation, bench_plastic_image);
        materialIcons.Add(Workstation.RubberStation, bench_rubber_image);
        materialIcons.Add(Workstation.GlassStation, bench_glass_image);
        materialIcons.Add(Workstation.ClothStation, bench_cloth_image);
        materialIcons.Add(Workstation.CircuitsStation, bench_circuits_image);

    }

    private void AddItemToUI(UIIngredient item)
    {
        UIUnitManager newUI = GameObject.Instantiate(UIPrefab, recipeContainer.transform);
        newUI.ingredient = item.ingredient;
        newUI.ingredientImage.sprite = UIPrefab.ingredient.icon;
        newUI.benchImage.sprite = materialIcons[item.station];

        recipeContainer.UpdateContainerSize();
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
