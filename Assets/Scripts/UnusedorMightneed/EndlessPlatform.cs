using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPlatform : MonoBehaviour {
    private Transform playertrans;
    private void Start()
    {
     playertrans = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        transform.position = Vector3.forward * playertrans.position.z;

    }

}
