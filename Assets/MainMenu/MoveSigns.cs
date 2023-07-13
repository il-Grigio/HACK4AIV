using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 movementDistance;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    [SerializeField] private float movingTime;

    private void Awake() {
        startingPosition = transform.position;    
        targetPosition = transform.position + movementDistance;    
    }

    private void Update() {
        float ratio = Mathf.Sin((Time.time / movingTime) - Mathf.PI * 0.5f) * 0.5f + 0.5f;
        transform.position = Vector3.Lerp(startingPosition, targetPosition, ratio);
    }
    private void OnDrawGizmosSelected() {
        Debug.DrawLine(startingPosition + transform.localScale * 0.5f, targetPosition + transform.localScale * 0.5f);
    }
}
