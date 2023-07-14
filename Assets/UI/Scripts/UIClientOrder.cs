using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UIClientOrder : MonoBehaviour
{
    [SerializeField] UICraftingUnit unitSample;
    private List<Recepie> ingredients;
    private List<UICraftingUnit> ingredientList;


    private void Awake()
    {
        ingredientList = new List<UICraftingUnit>();
    }

    public void NewRequest(Recepie newItem)
    {
        ingredients.Add(newItem);
        UICraftingUnit newUnit = GameObject.Instantiate(unitSample);
        newUnit.ingredient = newItem.recepie;
        newUnit.MaxTime = newItem.currentTime;
        ingredientList.Add(newUnit);

        SortItems();
    }

    public void SortItems()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>(true);
        System.Array.Sort(childTransforms, (a, b) => CompareChildrenByRemainingTime(a, b));
        for (int i = 0; i < childTransforms.Length; i++)
            childTransforms[i].SetSiblingIndex(i);
    }
    private int CompareChildrenByRemainingTime(Transform a, Transform b)
    {
        float timerA = a.GetComponent<UICraftingUnit>().currentTime;
        float timerB = b.GetComponent<UICraftingUnit>().currentTime;
        return timerA.CompareTo(timerB);
    }
}
