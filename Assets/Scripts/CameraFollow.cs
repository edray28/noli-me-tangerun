using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform LookAt;  //follow
    private Vector3 offset = new Vector3(0, 5.0f, -5.0f);
    private Vector3 moveVec;
    private float transition = 0.0f;
    private float animationDuration = 3.0f;
    private Vector3 animationOffset = new Vector3(0, 0, -10);
   

    // Use this for initialization
    void Start()
    {

        LookAt = GameObject.FindGameObjectWithTag("Player").transform;
        
        offset = transform.position - LookAt.position;


    }

    // Update is called once per frame
    private void LateUpdate()
    {

        
        moveVec = LookAt.position + offset;
        //x
        transform.position = LookAt.position + offset;
        moveVec.x = 0;
        transform.position = Vector3.Lerp(transform.position, moveVec, Time.deltaTime);
                                            
        //y
        moveVec.y = Mathf.Clamp(moveVec.y, -15, 8);   //animdefaltafteranim
       
        if (transition >1.0f)
        {
            transform.position = LookAt.position + offset;
        }
        else
        {
            transform.position = Vector3.Lerp(moveVec +animationOffset,moveVec,transition);
            transition += Time.deltaTime * 1 / animationDuration;
           
        }
        }

}
