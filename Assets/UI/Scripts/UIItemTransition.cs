using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemTransition : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    [SerializeField] private bool isMoving = false;
    //private bool isInDefaultState = true;

    private void Awake()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    public void Translate()
    {
        speed *= -1;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector3 step = offset.normalized * speed * Time.deltaTime;
            if (Vector3.SqrMagnitude(transform.position + step) <= Vector3.SqrMagnitude(transform.position - offset))
            {
                Debug.Log(Vector3.SqrMagnitude(step) + ", " + Vector3.SqrMagnitude(transform.position - offset));
                transform.position += step;
            }
            else
            {
                transform.position = defaultPosition + offset;
                isMoving = false;
            }
            
            
            //if (Vector3.SqrMagnitude(Mathf.Abs(newPos - ) - )
            //transform.position 
        }
    }

}
