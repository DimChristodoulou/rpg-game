using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed=0.75f;
    public Vector3 offset;

    void Start(){
        offset = new Vector3(-5, 5, -5);
    }

    void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") < 0){
            if(offset.x > -10)
                offset += new Vector3(-1,1,-1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0){
            if(offset.x < -5)
                offset -= new Vector3(-1,1,-1);
        }
    }

    void LateUpdate(){
        transform.position = target.position + offset;
    }
}
