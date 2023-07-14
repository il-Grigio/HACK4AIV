using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuButton : MonoBehaviour
{
    public MenuLabels label;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void ExecuteButton();
}
