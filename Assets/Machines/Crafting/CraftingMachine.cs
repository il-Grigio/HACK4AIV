using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMachine : MachineScript
{

    [SerializeField] private CraftingStand[] craftingStands;

    public override bool Interact() {
        //TODO craft item
        return true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
