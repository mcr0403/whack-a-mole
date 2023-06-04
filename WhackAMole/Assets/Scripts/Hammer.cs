using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public Camera mainCamera;

    public bool hit = false;
    public float xAxis = 0f;
    private void Update()
    {
        if(!hit)
        {
            Vector3 vec = new Vector3(Input.mousePosition.x  , Input.mousePosition.y, 16);
            transform.position = mainCamera.ScreenToWorldPoint(vec);
            if (Input.GetMouseButtonDown(0))
            {
                hit = true;
            }
        } else
        {

            if (transform.localEulerAngles.x <= 45)
            {
                transform.rotation = Quaternion.Euler(xAxis += Time.deltaTime*700, 0, 0);
            } else
            {
                hit = false;
                xAxis = 0f;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

    }

    

}
    
