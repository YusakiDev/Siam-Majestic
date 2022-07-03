using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelectionPlayer2 : MonoBehaviour
{
    private GameObject[] character2List;
    private int index;

    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
            
        character2List = new GameObject[transform.childCount];
        
        //fill the array with our model
        for (int i = 0; i < transform.childCount; i++)
        {
            character2List[i] = transform.GetChild(i).gameObject;
        }

        //we toogle off their renderer so we don't see them
        foreach (GameObject go in character2List)
        {
            go.SetActive(false);
        }
        
        //we toggle on the selected character
        if (character2List[index])
        {
            character2List[index].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        //Toggle off the current model
        character2List[index].SetActive(false);
        
        index--;
        if (index < 0)
        {
            index = character2List.Length - 1;
        }
        
        //Toggle on the ew model
        character2List[index].SetActive(true);
    }
    public void ToggleRight()
    {
        //Toggle off the current model
        character2List[index].SetActive(false);
        
        index++;
        if (index == character2List.Length)
        {
            index = 0;
        }
        
        //Toggle on the ew model
        character2List[index].SetActive(true);
    }

    //Change Scene Edit Scene Name here Dude
    public void ConfirmButton()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene(3);
    }
}
