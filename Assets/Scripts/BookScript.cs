using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour {

    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource audios;
    public bool alreadyPlayed = false;

    private Animator anim;

    private void Awake()
    {
        audios = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.SetTrigger("Spawn");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.StartPlayer();
            audios.clip = SoundToPlay;
            audios.Play();
            audios.volume = PlayerPrefs.GetFloat("SFX");
            anim.SetTrigger("Collected");


        }


    }
}
