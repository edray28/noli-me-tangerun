using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFollowPlatform : MonoBehaviour {
    private Vector3 offset = new Vector3(0,0,0);
    private Transform playertrans;
    private void Start()
    {

        playertrans = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playertrans.position;
        
    }
    private void Update()
    {
        transform.position = Vector3.back * playertrans.position.z;
        transform.position = playertrans.position + offset;

    }

}