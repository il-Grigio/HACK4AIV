using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Ingredient", menuName = "Ingredients")]
public class IngredientScriptable : ScriptableObject
{
    [SerializeField] public string fullName;
    [SerializeField] public GameObject model3D;
    [SerializeField] public Sprite icon;
    [SerializeField] public IngredientScriptable[] ingredients = new IngredientScriptable[0];

}
