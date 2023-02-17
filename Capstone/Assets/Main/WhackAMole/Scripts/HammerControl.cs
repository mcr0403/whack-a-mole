using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class HammerControl : MonoBehaviour
{
    public enum State
    {
        Available,
        Moving
    }
    public State state;
    public Vector3 destination;
    Vector3 offset = new Vector3(0, 0, 10);

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && state == State.Available)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (raycastHit.transform.gameObject.GetComponent<Mole>())
                {
                    destination = raycastHit.transform.position;
                    Debug.Log(raycastHit.transform.gameObject.name);
                }
            }
            state = State.Moving;
        }    
        if (state == State.Available)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset);
        }
        else if (state == State.Moving && transform.position != destination)
        {
            Move();
        }
        else
        {
            state = State.Available;
        }
    }
    void Move()
    {
        float step = 10 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
    }
}
