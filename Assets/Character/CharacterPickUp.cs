using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using static UnityEngine.UI.Image;
using static UnityEditor.Progress;

[AddComponentMenu("TopDown Engine/Character/Abilities/PickUp")]
public class CharacterPickUp : CharacterAbility
{
    /// This method is only used to display a helpbox text
    /// at the beginning of the ability's inspector
    public override string HelpBoxText() { return "TODO_HELPBOX_TEXT."; }

    [Header("TODO_HEADER")]

    [SerializeField] LayerMask pickuppableLayerMask;
    [SerializeField] LayerMask workStationLayerMask;
    [SerializeField] Transform itemStand;
    [SerializeField] Transform model;
    [SerializeField] float sphereCastRadius = 1.5f;
    /// the length of the ray to cast in front of the character to detect pushables
    [Tooltip("the length of the ray to cast in front of the character to detect pushables")]
    public float PhysicsInteractionsRaycastLength = 0.1f;
    /// the offset to apply to the origin of the physics interaction raycast (by default, the character's collider's center
    [Tooltip("the offset to apply to the origin of the physics interaction raycast (by default, the character's collider's center")]
    public Vector3 PhysicsInteractionsRaycastOffset = Vector3.zero;


    protected RaycastHit _hit;

    protected const string _yourAbilityAnimationParameterName = "YourAnimationParameterName";
    protected int _yourAbilityAnimationParameter;


    protected CharacterController _characterController;

    [Header("DEBUGGER")]
    [SerializeField] protected ItemComponent _itemComponent;
    [SerializeField] private bool hasItem = false;


    float hitDistance;

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

        if (_inputManager.TimeControlButton.State.CurrentState == MMInput.ButtonStates.ButtonDown) {
            if(hasItem)
                DropItemLogic();
            else
                PickUpItem();
        }
        if (_inputManager.TimeControlButton.State.CurrentState == MMInput.ButtonStates.ButtonUp) {
            //TimeControlStop();
        }


    }
    public void DropItemLogic() {
        if(!hasItem) { return; }

        Vector3 origin = _controller3D.transform.position + _characterController.center + model.up * PhysicsInteractionsRaycastOffset.y + model.right * PhysicsInteractionsRaycastOffset.x + model.forward * PhysicsInteractionsRaycastOffset.z;
        Vector3 direction = model.forward;
        float maxDistance = _characterController.radius + _characterController.skinWidth + PhysicsInteractionsRaycastLength;
        
        
        if (Physics.SphereCast(origin, sphereCastRadius, direction, out _hit,
                maxDistance, workStationLayerMask)) {
            MachineScript machine = _hit.transform.GetComponent<MachineScript>();
            if (machine.CanPlaceItems()) {
                machine.PlaceItem(_itemComponent);
                _itemComponent.transform.GetComponent<Rigidbody>().useGravity = false;
                _itemComponent.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                _itemComponent.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                _itemComponent.transform.GetComponent<Collider>().enabled = false;
                _itemComponent = null;
                hasItem = false;
            }
            return;
        }



        DropItem(_itemComponent);
        
        
        hasItem = false;
    }

    private void DropItem(ItemComponent item) {
        item.transform.parent = null;
        item.transform.GetComponent<Rigidbody>().useGravity = true;
        item.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        item.transform.GetComponent<Collider>().enabled = true;
        _itemComponent = null;
    }

    public void PickUpItem() {
        if (hasItem) return;

        Vector3 origin = _controller3D.transform.position + _characterController.center + model.up * PhysicsInteractionsRaycastOffset.y + model.right * PhysicsInteractionsRaycastOffset.x + model.forward * PhysicsInteractionsRaycastOffset.z;
        Vector3 direction = model.forward;
        float maxDistance = _characterController.radius + _characterController.skinWidth + PhysicsInteractionsRaycastLength;

        //checkWorkStation
        if(Physics.SphereCast(origin, sphereCastRadius, direction, out _hit,
                maxDistance, workStationLayerMask)) {

            MachineScript machine = _hit.transform.GetComponent<MachineScript>();
            _itemComponent = machine.GetFirstItem();
            machine.RemoveItem(_itemComponent);
            PutItemInArms(_itemComponent);
            return;
        }


        Physics.SphereCast(origin, sphereCastRadius, direction, out _hit,
                maxDistance, pickuppableLayerMask);
        hasItem = (_hit.collider != null);

        if (hasItem) {
            //TODO change arms animations
            _itemComponent = _hit.transform.GetComponent<ItemComponent>();
            PutItemInArms(_itemComponent);
        }
    }

    private void PutItemInArms(ItemComponent item) {
        if (item == null) return;
        hasItem = true;
        item.transform.parent = itemStand.transform;
        item.transform.GetComponent<Rigidbody>().useGravity = false;
        item.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        item.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        item.transform.GetComponent<Collider>().enabled = false;
        item.transform.position = itemStand.transform.position;
    }

    private void OnDrawGizmos() {

        Vector3 origin = _controller3D.transform.position + _characterController.center + model.up * PhysicsInteractionsRaycastOffset.y + model.right * PhysicsInteractionsRaycastOffset.x + model.forward * PhysicsInteractionsRaycastOffset.z;
        Vector3 direction = model.forward;
        float maxDistance = _characterController.radius + _characterController.skinWidth + PhysicsInteractionsRaycastLength;

        Physics.SphereCast(origin, sphereCastRadius, direction, out _hit,
                maxDistance, workStationLayerMask);
        hitDistance = (_hit.collider != null) ? _hit.distance : -1;

        // Visualize the spherecast shape in the Scene View
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + direction * maxDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, sphereCastRadius);
        Gizmos.DrawWireSphere(origin + direction * maxDistance, sphereCastRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(origin + direction * hitDistance, sphereCastRadius);
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
