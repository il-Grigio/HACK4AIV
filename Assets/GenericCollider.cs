using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCollider : MonoBehaviour
{
    public bool isColliding = false;
    [SerializeField] string tag = "Player";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision) {
        isColliding = collision.transform.tag == "tag";
    }
}
