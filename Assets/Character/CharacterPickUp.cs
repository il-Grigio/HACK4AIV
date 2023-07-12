using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

[AddComponentMenu("TopDown Engine/Character/Abilities/PickUp")]
public class CharacterPickUp : CharacterAbility
{
    /// This method is only used to display a helpbox text
    /// at the beginning of the ability's inspector
    public override string HelpBoxText() { return "TODO_HELPBOX_TEXT."; }

    [Header("TODO_HEADER")]

    [SerializeField] LayerMask pickuppableLayerMask;

    /// the length of the ray to cast in front of the character to detect pushables
    [Tooltip("the length of the ray to cast in front of the character to detect pushables")]
    public float PhysicsInteractionsRaycastLength = 0.1f;
    /// the offset to apply to the origin of the physics interaction raycast (by default, the character's collider's center
    [Tooltip("the offset to apply to the origin of the physics interaction raycast (by default, the character's collider's center")]
    public Vector3 PhysicsInteractionsRaycastOffset = Vector3.zero;


    protected RaycastHit _raycastHit;

    /// declare your parameters here
    public float randomParameter = 4f;
    public bool randomBool;

    protected const string _yourAbilityAnimationParameterName = "YourAnimationParameterName";
    protected int _yourAbilityAnimationParameter;


    protected CharacterController _characterController;


    private bool hasItem = false;

    /// <summary>
    /// Here you should initialize our parameters
    /// </summary>
    protected override void Initialization() {
        base.Initialization();
        randomBool = false;
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

        if (_inputManager.TimeControlButton.State.CurrentState == MMInput.ButtonStates.ButtonDown) {
            if(hasItem)
                DropItem();
            else
                PickUpItem();
        }
        if (_inputManager.TimeControlButton.State.CurrentState == MMInput.ButtonStates.ButtonUp) {
            //TimeControlStop();
        }


    }
    public void DropItem() {
        if(!hasItem) { return; }
        //TODO check if in front of a station
    }

    public void PickUpItem() {
        Physics.Raycast(_controller3D.transform.position + _characterController.center + PhysicsInteractionsRaycastOffset, _controller.CurrentMovement.normalized, out _raycastHit,
                _characterController.radius + _characterController.skinWidth + PhysicsInteractionsRaycastLength, pickuppableLayerMask);

        hasItem = (_raycastHit.collider != null);

        if (hasItem) {
            //TODO change arms animations
            //TODO place item in arms

        }
    }

    /// <summary>
    /// Adds required animator parameters to the animator parameters list if they exist
    /// </summary>
    protected override void InitializeAnimatorParameters() {
        RegisterAnimatorParameter(_yourAbilityAnimationParameterName, AnimatorControllerParameterType.Bool, out _yourAbilityAnimationParameter);
    }

    /// <summary>
    /// At the end of the ability's cycle,
    /// we send our current crouching and crawling states to the animator
    /// </summary>
    public override void UpdateAnimator() {

        bool myCondition = true;
        MMAnimatorExtensions.UpdateAnimatorBool(_animator, _yourAbilityAnimationParameter, myCondition, _character._animatorParameters);
    }

}
