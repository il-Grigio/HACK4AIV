using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("TopDown Engine/Character/Abilities/SideMenu")]
public class CharacterSideMenu : CharacterAbility {
    protected float _verticalMovement;
    protected override void HandleInput() {
        // here as an example we check if we're pressing down
        // on our main stick/direction pad/keyboard
        if (_inputManager.ReloadButton.State.CurrentState == MMInput.ButtonStates.ButtonDown) {
            //Your Method

        }

        //_horizontalMovement = _horizontalInput;
        _verticalMovement = _verticalInput;
    }
}
