using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingControl : MonoBehaviour {

    public GameObject loadingscreen;
    public Slider slider;
    AsyncOperation async;
 

    public void LoadScreen()
    {
        StartCoroutine(LoadingScreen());
        {

        }
    }
    IEnumerator LoadingScreen()
    {
        loadingscreen.SetActive(true);
        async = SceneManager.LoadSceneAsync(0);
        async.allowSceneActivation = false;
        while (async.isDone == false)
        {
            slider.value = async.progress;
            if(async.progress ==0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;

        }
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
