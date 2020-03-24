using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private Transform target; //target to follow - in our case, it's player
    public float smoothSpeed;
    public float boundX;
    public float boundY;

    private Vector3 velocity = Vector3.zero;

    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {

        Vector3 moveDelta = Vector3.zero; // how much camera will move
        Vector3 delta = target.position - transform.position;

        //X axis
        if (delta.x > boundX) {
            moveDelta.x = delta.x - boundX;
        }
        else if (delta.x < -boundX) {
            moveDelta.x = delta.x + boundX;
        }

        //Y axis
        if (delta.y > boundY) {
            moveDelta.y = delta.y - boundY;
        } else if (delta.y < -boundY) {
            moveDelta.y = delta.y + boundY;
        }

        Vector3 desiredPosition = transform.position + moveDelta;
        //Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.deltaTime);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(boundX * 2, boundY * 2, 0f));
    }

}
