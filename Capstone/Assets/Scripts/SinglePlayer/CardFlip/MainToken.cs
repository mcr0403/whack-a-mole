using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainToken : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    GameObject gameControl;
    public Sprite[] face;
    public Sprite back;
    public int faceIndex;
    public bool twoCardUp = false;
  
    public void OnMouseDown()
    {
        if (spriteRenderer.sprite == back && gameControl.GetComponent<Control>().checkTwoCard() == false)
        {
            spriteRenderer.sprite = face[faceIndex];
            gameControl.GetComponent<Control>().imageOpened(this);
            Debug.Log(twoCardUp);

        }
    }

    public void Close()
    {
        spriteRenderer.sprite = back;
    }


    private void Awake()
    {
        gameControl = GameObject.Find("GameControl");
        spriteRenderer= GetComponent<SpriteRenderer>();
    }
}
