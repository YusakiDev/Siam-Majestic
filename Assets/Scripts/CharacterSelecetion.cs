using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelecetion : MonoBehaviour
{
    private GameObject[] characterList;
    private int index;

    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
            
        characterList = new GameObject[transform.childCount];
        
        //fill the array with our model
        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        //we toogle off their renderer so we don't see them
        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }
        
        //we toggle on the selected character
        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        //Toggle off the current model
        characterList[index].SetActive(false);
        
        index--;
        if (index < 0)
        {
            index = characterList.Length - 1;
        }
        
        //Toggle on the ew model
        characterList[index].SetActive(true);
    }
    public void ToggleRight()
    {
        //Toggle off the current model
        characterList[index].SetActive(false);
        
        index++;
        if (index == characterList.Length)
        {
            index = 0;
        }
        
        //Toggle on the ew model
        characterList[index].SetActive(true);
    }

    //Change Scene Edit Scene Name here Dude
    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene("PlayScene");
    }
}
