using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSoundReal : MonoBehaviour {
    AudioSource audios;
    public AudioClip dead;
    // Use this for initialization
    void Start () {
        audios = GetComponent<AudioSource>();
        audios.PlayOneShot(dead);
        audios.volume = PlayerPrefs.GetFloat("Volume");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
