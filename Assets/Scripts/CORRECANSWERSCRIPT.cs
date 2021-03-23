using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CORRECANSWERSCRIPT : MonoBehaviour
{
    public GameObject lol;
    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource audios;
    public bool alreadyPlayed = false;
    public Animator anim;
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
            GameManager.Instance.GetCorrectAnswer();
            audios.clip = SoundToPlay;
            audios.Play();
            audios.volume = PlayerPrefs.GetFloat("SFX");
            anim.SetTrigger("Collected");
            Invoke("DeleteLetter",1.0f);

        }
    }
    public void DeleteLetter()
    {
        lol.SetActive(false);
    }
}
