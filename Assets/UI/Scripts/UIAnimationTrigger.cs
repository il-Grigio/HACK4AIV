using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Space]
    [SerializeField] public string paramName;

    private bool status = false;



    public void TriggerAnimation()
    {
        status = !status;
        animator.SetBool(paramName, status);
    }
}
