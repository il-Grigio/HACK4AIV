using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    public IngredientScriptable ingredientScriptable;
    Rigidbody rb;
    private void Awake() {
        rb = GetComponent<Rigidbody> ();
    }
    private void Update() {
        if (!rb.useGravity) {
            transform.localPosition = Vector3.zero;
        }
    }
}
