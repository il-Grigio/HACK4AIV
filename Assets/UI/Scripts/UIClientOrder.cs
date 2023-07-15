using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class UIClientOrder : MonoBehaviour
{
    [SerializeField] UICraftingUnit unitSample;
    [SerializeField] Transform UIContainer;
    private List<Recepie> ingredients;
    private List<UICraftingUnit> ingredientList;


    private void Awake()
    {
        ingredientList = new List<UICraftingUnit>();
        ingredients = new List<Recepie>();
    }

    public void NewRequest(Recepie newItem)
    {
        ingredients.Add(newItem);
        UICraftingUnit newUnit = GameObject.Instantiate(unitSample, UIContainer);
        newUnit.ingredient = newItem.recepie;
        newUnit.MaxTime = newItem.timeToFinishRecepie;
        ingredientList.Add(newUnit);

        if (ingredients.Count > 1) 
        {
            SortItems();
        }
    }

    public void RemoveItem(Recepie item)
    {
        ingredients.Remove(item);
        for (int i = 0; i < ingredientList.Count; i++) {
            if(item.recepie == ingredientList[i].ingredient) {
                ingredientList[i].RemoveItem();
                ingredientList.RemoveAt(i);

                return;
            }
        }
    }

    public void SortItems()
    {
        List<Transform> tempTransform = new List<Transform>();
        foreach(var item in ingredientList)
        {
            tempTransform.Add(item.transform);
        }
        Transform[] childTransforms = tempTransform.ToArray();
        System.Array.Sort(childTransforms, (a, b) => CompareChildrenByRemainingTime(a, b));
        for (int i = 0; i < childTransforms.Length; i++)
            childTransforms[i].SetSiblingIndex(i);
    }
    private int CompareChildrenByRemainingTime(Transform a, Transform b)
    {
        float timerA = a.GetComponent<UICraftingUnit>().currentTime;
        float timerB = b.GetComponent<UICraftingUnit>().currentTime;
        return timerB.CompareTo(timerA);
    }
}
