using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
 

        //Main
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
        if(other.tag == "Player")
        {
            GameManager.Instance.GetCoin();
            
            audios.clip = SoundToPlay;
            audios.Play();
       //FIX SOUND ISSUE     audios.volume = PlayerPrefs.GetFloat("SFX");
            anim.SetTrigger("Collected");
            

        }
        

    }
   

    
}
