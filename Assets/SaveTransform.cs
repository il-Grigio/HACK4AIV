using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTransform : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StaticClass.playerTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
