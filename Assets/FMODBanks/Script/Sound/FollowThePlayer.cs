using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePlayer : MonoBehaviour
{
    bool hasParent = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(StaticClass.playerTransform != null && !hasParent) 
        {
            transform.parent = StaticClass.playerTransform;
            transform.position = StaticClass.playerTransform.position;
            hasParent = true;

        }
    }
}
