using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggle_help : MonoBehaviour
{
    [SerializeField] GameObject HelpPanel;

    public void Help()
    {
        HelpPanel.SetActive(true);
        
    }
    
    public void NoHelp()
    {
        HelpPanel.SetActive(false);
        
    }
}
