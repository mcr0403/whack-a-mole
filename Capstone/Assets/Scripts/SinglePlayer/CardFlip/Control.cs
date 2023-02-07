using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Control : MonoBehaviour
{
    GameObject token;
    List<int> faceIndexes = new List<int> { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4};
    public static System.Random rdm = new System.Random();
    public int shuffleNum = 0;
    private float timer = 0f;
    private float temp = 0f;
    private bool firstChoice = false;
    private int count = 0;


    public TMP_Text score;

    private void Start()
    {
        float yPosition = 2.5f;
        float xPosition = -7.0f;
        for (int i = 0; i < 10; i++)
        {
            int cardNum = faceIndexes.Count;
            shuffleNum = rdm.Next(0, cardNum);
            var temp = Instantiate(token, new Vector3(xPosition, yPosition,0),
                Quaternion.identity);
            temp.GetComponent<MainToken>().faceIndex = faceIndexes[shuffleNum];
            faceIndexes.Remove(faceIndexes[shuffleNum]);

            xPosition = xPosition + 3.5f;
            if ( i == 4)
            {
                xPosition = -7f;
                yPosition = -1.0f;
            }
        }

    }

    private MainToken FirstCard;
    private MainToken SecondCard;

    public void imageOpened(MainToken card)
    {
        
        if (FirstCard == null)
        {
            if (!firstChoice)
                firstChoice = true;
            FirstCard = card;
            Debug.Log("set first card");
        }
        else
        {  
            Debug.Log("set second card");
            SecondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    public IEnumerator CheckMatch()
    {
        Debug.Log("test");
        if (FirstCard.faceIndex == SecondCard.faceIndex)
        {
            count++;

        } else {
            yield return new WaitForSeconds(0.5f);

            FirstCard.Close();
            SecondCard.Close();
            
        }
        FirstCard = null;
        SecondCard= null;
    }

    public bool checkTwoCard()
    {
        bool cardsUp = false;
        if (FirstCard != null && SecondCard != null)
        {
            cardsUp = true;
        }
        return cardsUp;
    }

    private void Update()
    {
        if (firstChoice == true)
        {
            timer += Time.deltaTime;
            temp = (float)Math.Round(timer);
            score.text = temp.ToString();
        }

        if (count == 5)
        {
            firstChoice = false;
        }

    }

    public void Reset()
    {
        SceneManager.LoadScene(2);
    }

    private void Awake()
    {
        token = GameObject.Find("Token");
    }
}
