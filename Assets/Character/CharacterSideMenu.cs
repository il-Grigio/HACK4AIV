using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("TopDown Engine/Character/Abilities/SideMenu")]
public class CharacterSideMenu : CharacterAbility {
    protected float _verticalMovement;
    private UIAnimationTrigger trigger;
    private CharacterMovement charMoveScript;
    private bool isToggled = false;

    protected override void Initialization() {
        base.Initialization();
        trigger = GameObject.Find("RecipesMenu").GetComponent<UIAnimationTrigger>();
        charMoveScript = GetComponent<CharacterMovement>();
    }
    

    protected override void HandleInput() {
        // here as an example we check if we're pressing down
        // on our main stick/direction pad/keyboard
        if (_inputManager.ReloadButton.State.CurrentState == MMInput.ButtonStates.ButtonDown) {
            trigger.TriggerAnimation();
        }
        if (isToggled)
        {
            _verticalMovement = _verticalInput;
        }
    }

    public void ToggleInputs()
    {
        isToggled = !isToggled;
        charMoveScript.InputAuthorized = !isToggled;
    }
}
