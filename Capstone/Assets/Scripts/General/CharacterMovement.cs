using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 5f;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        Vector3 move = new Vector3();
        if (Input.GetKey(KeyCode.W)) move.z += 1;
        if (Input.GetKey(KeyCode.S)) move.z += -1;
        if (Input.GetKey(KeyCode.A)) move.x += -1;
        if (Input.GetKey(KeyCode.D)) move.x += 1;
        characterController.Move(move * Time.deltaTime * speed);
    }
}
