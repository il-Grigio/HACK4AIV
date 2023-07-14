using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Space]
    [SerializeField] public string paramName;

    private CharacterSideMenu charMenuController;

    private bool status = false;

    public void TriggerAnimation()
    {
        status = !status;
        animator.SetBool(paramName, status);
        if (charMenuController == null) charMenuController = GameObject.Find("Robottino").GetComponent<CharacterSideMenu>();
        charMenuController.ToggleInputs();
    }
}
