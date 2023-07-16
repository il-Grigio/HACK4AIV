using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience Sound")]
    [field: SerializeField] public EventReference ambienceSound { get; private set; }

    [field: Header("Music Menu")]
    [field: SerializeField] public EventReference musicMenu { get; private set; }

    [field: Header("Speech")]
    [field: SerializeField] public EventReference speechEvent { get; private set; }

    [field: Header("ChainSawTable")]
    [field: SerializeField] public EventReference ChainSawTable { get; private set; }

    [field: Header("pickUpItem")]
    [field: SerializeField] public EventReference pickUpItem { get; private set;}
    [field: Header("DropDownItem")]
    [field: SerializeField] public EventReference dropItem { get; private set; }

    [field: Header("PlaceItemInTheCrafting")]
    [field: SerializeField] public EventReference placeItemInCrafting { get; private set; }
    [field: Header("footsSteps")]
    [field: SerializeField] public EventReference footsStep { get; private set; }

    [field: Header("loomTable")]
    [field: SerializeField] public EventReference loomTable { get; private set; }
    public static FMODEvents instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("FMODEvents already exist in the scene");

        }
        instance = this;
    }
}
