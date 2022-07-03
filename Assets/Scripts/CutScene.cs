using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(2);
        }
    }
}
