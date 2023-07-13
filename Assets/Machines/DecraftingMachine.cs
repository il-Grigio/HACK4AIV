using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecraftingMachine : MachineScript
{
    [SerializeField] private float decraftingTime;
    [SerializeField] private ItemComponent materialDrop;
    [SerializeField] private float currentDecraftingTime;
    private bool isStarted;
    private float itemRotationSpeed;
    [SerializeField] private float itemThrowForce;

    private void Update() {
        if (isStarted) {
            currentDecraftingTime -= Time.deltaTime;
            if(currentDecraftingTime <= 0) {
                isStarted = false;
                SpawnMaterial();
                Destroy(placedItems[0].gameObject);
                placedItems[0] = null;
            }
        }
    }

    //When the player interacts with the machine, use this function (Es. start crafting)
    public override bool Interact() {
        if (placedItems[0] && placedItems[0].ingredientScriptable.ingredients.Length > 0) {
            if (CheckCorrectMaterial()) {
                currentDecraftingTime = decraftingTime;
                isStarted = true;
                return true;
            }
        }
        return false;
    }

    //Spawns material with a small hop
    private void SpawnMaterial() {
        ItemComponent item = Instantiate<ItemComponent>(materialDrop);
        item.transform.position = placeItemPositions[0].position;
        Rigidbody itemrb = item.GetComponent<Rigidbody>();
        itemrb.AddForce(-transform.forward * itemThrowForce);
    }

    private bool CheckCorrectMaterial() {
        foreach(IngredientScriptable item in placedItems[0].ingredientScriptable.ingredients) {
            if(string.Compare(item.fullName, materialDrop.ingredientScriptable.fullName) == 0) {
                Debug.Log("Material found");
                return true;
            }
        }
        Debug.Log("Material not found");
        return false;
    }

#if UNITY_EDITOR
    private void OnGUI() {
        if(GUI.Button(new Rect(10, 10, 60, 30), "Interact")) {
            Interact();
        }
    }
#endif
}
