using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("TopDown Engine/Character/Abilities/Interact")]
public class CharacterInteract : CharacterAbility {
    /// This method is only used to display a helpbox text
    /// at the beginning of the ability's inspector
    public override string HelpBoxText() { return "TODO_HELPBOX_TEXT."; }

    [Header("TODO_HEADER")]
    [SerializeField] LayerMask workStationLayerMask;
    [SerializeField] Transform model;
    [SerializeField] float sphereCastRadius = 1.5f;
    [Tooltip("the length of the ray to cast in front of the character to detect pushables")]
    public float PhysicsInteractionsRaycastLength = 0.1f;
    /// the offset to apply to the origin of the physics interaction raycast (by default, the character's collider's center
    [Tooltip("the offset to apply to the origin of the physics interaction raycast (by default, the character's collider's center")]
    public Vector3 PhysicsInteractionsRaycastOffset = Vector3.zero;

    protected RaycastHit _hit;
    protected CharacterController _characterController;
    /// <summary>
    /// Here you should initialize our parameters
    /// </summary>
    protected override void Initialization() {
        base.Initialization();
        _characterController = _controller.GetComponent<CharacterController>();
    }

    /// <summary>
    /// Every frame, we check if we're crouched and if we still should be
    /// </summary>
    public override void ProcessAbility() {
        base.ProcessAbility();
    }

    /// <summary>
    /// Called at the start of the ability's cycle, this is where you'll check for input
    /// </summary>
    protected override void HandleInput() {
        // here as an example we check if we're pressing down
        // on our main stick/direction pad/keyboard
        if (_inputManager.InteractButton.State.CurrentState == MMInput.ButtonStates.ButtonDown) {
            CheckMachine();
        }
    }


    private void CheckMachine() {

        Vector3 origin = _controller3D.transform.position + _characterController.center + model.up * PhysicsInteractionsRaycastOffset.y + model.right * PhysicsInteractionsRaycastOffset.x + model.forward * PhysicsInteractionsRaycastOffset.z;
        Vector3 direction = model.forward;
        float maxDistance = _characterController.radius + _characterController.skinWidth + PhysicsInteractionsRaycastLength;


        if (Physics.SphereCast(origin, sphereCastRadius, direction, out _hit,
            maxDistance, workStationLayerMask)) {
            MachineScript machine = _hit.transform.GetComponent<MachineScript>();
            machine.Interact();
            return;
        }
    }

    /// <summary>
    /// If we're pressing down, we check for a few conditions to see if we can perform our action
    /// </summary>
    protected virtual void DoSomething() {
        // if the ability is not permitted
        if (!AbilityPermitted
            // or if we're not in our normal stance
            || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)
            // or if we're grounded
            || (!_controller.Grounded)) {
            // we do nothing and exit
            return;
        }

        // if we're still here, we display a text log in the console
        MMDebug.DebugLogTime("We're doing something yay!");
    }

    /// <summary>
    /// Adds required animator parameters to the animator parameters list if they exist
    /// </summary>
    protected override void InitializeAnimatorParameters() {
        //RegisterAnimatorParameter(_yourAbilityAnimationParameterName, AnimatorControllerParameterType.Bool, out _yourAbilityAnimationParameter);
    }

    /// <summary>
    /// At the end of the ability's cycle,
    /// we send our current crouching and crawling states to the animator
    /// </summary>
    public override void UpdateAnimator() {

        //MMAnimatorExtensions.UpdateAnimatorBool(_animator, _yourAbilityAnimationParameter, myCondition, _character._animatorParameters);
    }
}

