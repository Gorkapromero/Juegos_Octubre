using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl_inGame : MonoBehaviour {

    public Transform target;
    public Transform targetLookAt;

    public float rotacionY;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public bool camaraLibre = false;
    
    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (!camaraLibre)
        {
            Vector3 desiredPosition = target.position + offset; // - new Vector3(-20.0f, 0f,0f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Vector3 target2 = new Vector3(targetLookAt.position.x, rotacionY, 0);
            transform.LookAt(target2);
            //transform.rotation *= Quaternion.Euler(-30,0,0);
        }
    }
       
}