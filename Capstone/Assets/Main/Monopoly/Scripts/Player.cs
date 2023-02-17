using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 point;
    public Vector3 direction;
    public Vector3 offset;
    public List<GameObject> path = new List<GameObject>();
    void Update()
    {
        if (path.Count > 0)
        {
            point = new Vector3(path[path.Count - 1].transform.position.x, 0, path[path.Count - 1].transform.position.z);
            if (transform.position != point)
            {
                MoveCharacter();
            }
            else path.Remove(path[path.Count - 1]);
        }
    }

    void MoveCharacter()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, point, step);
    }

    public void SetPath(int diceNumber)
    {
        for(int i = diceNumber; i > 0; i--)
        {

        }
    }
}
