using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Ingredient", menuName = "Ingredients")]
public class IngredientScriptable : ScriptableObject
{
    [SerializeField] string fullName;
    [SerializeField] GameObject model3D;
    [SerializeField] List<IngredientScriptable> myIngredients = new List<IngredientScriptable>();
}
