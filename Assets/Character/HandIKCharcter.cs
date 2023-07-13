using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[AddComponentMenu("Locomotion/IK/Hands IK")]
public class HandIKCharcter : MonoBehaviour
{
    public enum Side : byte { Left, Right }
    private Animator _animator = null;

    Vector3 leftTargetPosition, rightTargetPosition;
    Quaternion leftTargetRotation, rightTargetRotation;

    [HideInInspector] public bool automatic = true;
    private Animator animator
    {
        get
        {
            _animator = GetComponent<Animator>();
            return _animator;
        }
    }

    public void SetIK(Side hand, Vector3 targetPos, Quaternion TargetRot) 
    {
        automatic = false;
        if(hand== Side.Left) 
        {
            leftTargetPosition = targetPos;
            leftTargetRotation = TargetRot;
        }
        else 
        {
            rightTargetPosition = targetPos;
            rightTargetRotation = TargetRot;
        }
    }

    void SetNotAutoIk(Side hand, Vector3 targetPos, Quaternion targetRot) 
    {
        AvatarIKGoal handGoal = hand == Side.Left ? AvatarIKGoal.LeftHand : AvatarIKGoal.RightHand;
        animator.SetIKPosition(handGoal, targetPos);
        animator.SetIKRotation(handGoal, targetRot);
        animator.SetIKPositionWeight(handGoal,1);
        animator.SetIKRotationWeight(handGoal, 1);
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (!automatic)
        {
            SetNotAutoIk(Side.Left, leftTargetPosition, leftTargetRotation);
            SetNotAutoIk(Side.Right, rightTargetPosition, rightTargetRotation);
        }

    }
}
