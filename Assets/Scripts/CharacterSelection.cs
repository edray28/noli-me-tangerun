using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    public int index;
    private void Start()
    {
        characterList = new GameObject[transform.childCount];
     for(int i =0; i <transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;

            foreach (GameObject go in characterList)
                go.SetActive(false);

            if (characterList[0])
                characterList[0].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        characterList[index].SetActive(false);
        index--;
        if (index < 0)
            index = characterList.Length - 1;

        characterList[index].SetActive(false);
    }

    public void Confirmbutton()
    {
        characterList[index].SetActive(false);
        index--;
        if (index < 0)
            index = characterList.Length - 1;

        characterList[index].SetActive(false);
    
        SceneManager.LoadScene("MainMenu");
    }

}