using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = Vector2.zero;

        moveVector.x = Input.GetAxis("Horizontal") * 5;
        moveVector.y = Input.GetAxis("Vertical") * 5;

        controller.Move(moveVector * Time.deltaTime);
    }
}
