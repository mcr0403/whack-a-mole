using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainToken : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    GameObject gameControl;
    public List<Sprite> imageCard;
    public List<string> nameCard;
    public Sprite back;
    public string faceIndex;
    public bool twoCardUp = false;
    public int count;

    public string lessonName;
    public Vocabulary[] vocabularies;
    public List<Vocabulary> learningLesson;
    public int[] indexArr;
    public int index;

    public TMP_Text text;

    private void Start()
    {
        foreach (var vocab in vocabularies)
        {
            if (vocab.lessonName == lessonName)
            {
                learningLesson.Add(vocab);
            }
        }

        foreach(int i in indexArr)
        {
            imageCard.Add(learningLesson[i].image);
            nameCard.Add(learningLesson[i].name);
        }


    }

    public void OnMouseDown()
    {
        if (spriteRenderer.sprite == back && gameControl.GetComponent<Control>().checkTwoCard() == false)
        {
            string[] temp = faceIndex.Split('-');
            if (temp[0] == "N")
            {
                index = Int32.Parse(temp[1]);
                spriteRenderer.sprite = null;
                text.text = nameCard[index];
            }else
            {
                index = Int32.Parse(temp[1]);
                spriteRenderer.sprite = imageCard[index];
            }
            
            gameControl.GetComponent<Control>().imageOpened(this);         
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
