using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{

    // Static fields can be accessed from anywhere, and don't belong
    // to any of the class instances.
    public static string selectedCharacterName;

    [SerializeField] GameObject characterOne;
    [SerializeField] GameObject characterTwo;

    void Start()
    {
        if (selectedCharacterName == "characterOneName")
        {
            characterOne.SetActive(true);
        }
        else if (selectedCharacterName == "characterTwoName")
        {
            characterTwo.SetActive(true);
        }
        else
        {
            // If nothing works, load character one as a default.
            // Useful for loading the scene by itself for testing.
            characterOne.SetActive(true);
        }
    }
}
