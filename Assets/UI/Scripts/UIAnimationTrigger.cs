using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Space]
    [SerializeField] public string paramName;
    [SerializeField] private GameObject toggleMenu;
    private CharacterSideMenu charMenuController;

    public bool status = false;
    

    public void TriggerAnimation()
    {
        if (!toggleMenu.activeSelf) return;
        status = !status;
        animator.SetBool(paramName, status);
        if (charMenuController == null) charMenuController = GameObject.Find("Robottino").GetComponent<CharacterSideMenu>();
        charMenuController.ToggleInputs();
    }
}
