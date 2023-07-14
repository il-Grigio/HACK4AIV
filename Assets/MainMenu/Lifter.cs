using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifter : MonoBehaviour
{
    [SerializeField] private SpringJoint joint;
    private Rigidbody jointRb;

    [SerializeField] private float liftSpeed;
    [SerializeField] private float liftMaxDistance;
    [SerializeField] private bool isLifted;

    private void Awake() {
        jointRb = joint.gameObject.GetComponent<Rigidbody>();
        if (!isLifted) {
            liftMaxDistance = joint.maxDistance;
        }
    }

    private void Update() {
        joint.maxDistance = !isLifted ? Mathf.Lerp(joint.maxDistance, liftMaxDistance, liftSpeed * Time.deltaTime) : Mathf.Lerp(joint.maxDistance, 0, liftSpeed * Time.deltaTime);
        jointRb.WakeUp();
    }

    public void LiftUp() {
        isLifted = true;
    }

    public void LiftDown() {
        isLifted = false;
    }
}
