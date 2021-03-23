using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
    public Text timer;
    public static float countdown = 3.0f;
	// Use this for initialization
	void Start () {
    timer= GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        
        
            countdown -= Time.deltaTime;
            if (countdown < 0)
                countdown = 0;
            timer.text = "" + Mathf.Round(countdown);

        

	}
}
