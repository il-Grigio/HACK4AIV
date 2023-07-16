using Grigios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsObjectPool : Singleton<ItemsObjectPool>
{
    [SerializeField] List<ItemComponent> items = new List<ItemComponent>();
    [SerializeField] int initialDuplicates;
    Dictionary<IngredientScriptable, List<ItemComponent>> ingredients;

    private void Awake() {
        ingredients = new Dictionary<IngredientScriptable, List<ItemComponent>>();
        foreach (var item in items) {
            item.gameObject.SetActive(false);
            if (ingredients.ContainsKey(item.ingredientScriptable)) {
                //Debug.LogError("Repetition of item: " + item.ingredientScriptable + "..." + item);
                ingredients[item.ingredientScriptable].Add(item);
                continue;
            }
            List<ItemComponent> list = new List<ItemComponent> ();
            list.Add(item);
            ingredients.Add(item.ingredientScriptable, list);
        }
        foreach (List<ItemComponent> item in ingredients.Values) {
            for (int i = 0; i < initialDuplicates; i++) {
                ItemComponent o = Instantiate(item[0], transform);
                item.Add(o);
            }
        }

    }

    public ItemComponent GetItem(IngredientScriptable ingredientScriptable, bool autoSetActive = true) {
        if(!ingredients.ContainsKey (ingredientScriptable)) return null;
        ItemComponent item = null;
        for (int i = 1; i < ingredients[ingredientScriptable].Count; i++) {
            if (!ingredients[ingredientScriptable][i].isActiveAndEnabled)
                item = ingredients[ingredientScriptable][i];
        }

        if(item == null) {
            item = Instantiate(ingredients[ingredientScriptable][0], transform);
            ingredients[ingredientScriptable].Add(item);
        }
        if(autoSetActive)item.gameObject.SetActive(true);
        return item;
    }

}
