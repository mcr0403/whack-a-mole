using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public int moveRange = 3;
    public float speed = 5f;
    public Vector3 point;
    public Vector3 direction;
    public GameObject model;
    public List<GameObject> path = new List<GameObject>();

    void Update()
    {
        if (path.Count > 0)
        {
            point = new Vector3(path[path.Count - 1].transform.position.x, 0, path[path.Count - 1].transform.position.z);
            direction = (point - transform.position).normalized;
            if (transform.position != point)
            {
                MoveCharacter();
                model.transform.forward = direction;
            }
            else path.Remove(path[path.Count - 1]);
        }
    }

    void MoveCharacter()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, point, step);
    }
}
