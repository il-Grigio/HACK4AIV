using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCollider : MonoBehaviour
{
    public bool isColliding = false;
    [SerializeField] string _tag = "Player";
    
    //private void OnTriggerStay(Collider other) {
    //    isColliding = (other.transform.tag == _tag);
        
    //}

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == _tag)
            isColliding = true;
    }
    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == _tag)
            isColliding = false;
        
    }

}
