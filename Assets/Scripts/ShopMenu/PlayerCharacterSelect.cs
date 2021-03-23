using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterSelect : MonoBehaviour {
    public GameObject maria;
    int ischarsold;

    public void Start()
    {
        ischarsold = PlayerPrefs.GetInt("ischarsold");
        if (ischarsold == 1)
            //Ibarra
            maria.SetActive(true);
        else
            maria.SetActive(false);

    }


}
