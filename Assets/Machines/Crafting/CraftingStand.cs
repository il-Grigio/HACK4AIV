using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStand : MachineScript
{
    public override bool Interact() {
        return true;
    }

    public ItemComponent getPlacedItem() { if (placedItems[0]) { return placedItems[0]; } return null; }
}
