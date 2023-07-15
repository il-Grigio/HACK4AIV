using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContextMenu : MonoBehaviour
{

    [SerializeField] private RectTransform ContextAction_Pickup;
    [SerializeField] private RectTransform ContextAction_Interact;
    [Space]
    [SerializeField] private Vector2 offset;

    private Transform target;
    private Animator animator_PU;
    private Animator animator_In;
    private bool following = false;

    private void Awake()
    {
        animator_PU = ContextAction_Pickup.GetComponent<Animator>();
        animator_In = ContextAction_Interact.GetComponent<Animator>();
        CloseMenu();
    }

    public void CloseMenu()
    {
        ContextAction_Pickup.gameObject.SetActive(false);
        ContextAction_Interact.gameObject.SetActive(false);
    }

    public void OpenMenu(Transform _target, bool interactable)
    {
        target = _target;
        following = true;
        ContextAction_Pickup.gameObject.SetActive(true);
        animator_PU.SetBool("PlayAnimation", true);
        if (interactable)
        {
            ContextAction_Interact.gameObject.SetActive(true);
            animator_In.SetBool("PlayAnimation", true);
        }
    }

    private void Update()
    {
        if (following)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position) + new Vector3(offset.x, offset.y, 0);
    }
}
