using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int diceNumber = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity == Vector3.zero)
        {
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            transform.position = new Vector3(0, 2, 0);
            rb.AddForce(Vector3.up * 500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
        if (rb.velocity == Vector3.zero && diceNumber == 0)
        {
            if (Vector3.Angle(Vector3.up, transform.up) == 0) diceNumber = 4;
            else if (Vector3.Angle(Vector3.up, -transform.up) == 0) diceNumber = 3;
            else if (Vector3.Angle(Vector3.up, transform.right) == 0) diceNumber = 1;
            else if (Vector3.Angle(Vector3.up, -transform.right) == 0) diceNumber = 6;
            else if (Vector3.Angle(Vector3.up, transform.forward) == 0) diceNumber = 2;
            else if (Vector3.Angle(Vector3.up, -transform.forward) == 0) diceNumber = 5;
            else
            {
                float dirX = Random.Range(-100, 100);
                float dirY = Random.Range(-100, 100);
                float dirZ = Random.Range(-100, 100);
                rb.AddTorque(dirX, dirY, dirZ);
            }
            if (diceNumber != 0) Debug.Log(diceNumber);
        }
        if (rb.velocity != Vector3.zero && diceNumber != 0)
        {
            diceNumber = 0;
        }
    }
}
