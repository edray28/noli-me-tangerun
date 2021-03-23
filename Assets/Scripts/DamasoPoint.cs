using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamasoPoint : MonoBehaviour {
    private Animator anim;
    private float speed = 10.0f;
    private CharacterController controller;
    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
      //  Vector3 moveVector = Vector3.zero;
        controller.Move((Vector3.forward * speed)*Time.deltaTime);
    }
}
