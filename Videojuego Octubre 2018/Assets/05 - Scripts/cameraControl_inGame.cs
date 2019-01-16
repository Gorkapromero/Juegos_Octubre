using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl_inGame : MonoBehaviour {

    public Transform target;
    public Transform targetLookAt;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    
    
    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // - new Vector3(-20.0f, 0f,0f);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(targetLookAt);
        //transform.rotation *= Quaternion.Euler(-30,0,0);
    }



}

