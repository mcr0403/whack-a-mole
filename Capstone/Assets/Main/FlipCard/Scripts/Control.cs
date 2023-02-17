using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Control : MonoBehaviour
{
    GameObject token;
    List<string> faceIndexes = new List<string>() {"N-0", "N-1", "N-2", "N-3", "N-4", "I-0", "I-1", "I-2", "I-3", "I-4" };

    public static System.Random rdm = new System.Random();
    public int shuffleNum = 0;
    public int index = 0;
    public int[] id = new int[5];

    private float timer = 0f;
    private float temp = 0f;
    private bool start = false;
    private int count = 0;
    

    public TMP_Text score;

    private void Start()
    {
        int[] randomNumbers = Enumerable.Range(0, 19).OrderBy(x => Guid.NewGuid()).Take(5).ToArray();

        token.GetComponent<MainToken>().indexArr = randomNumbers;

        float yPosition = 2.5f;
        float xPosition = -8.0f;

        for (int i = 0; i < 10; i++)
        {
            int cardNum = faceIndexes.Count;
            shuffleNum = rdm.Next(0, cardNum);
            var temp = Instantiate(token, new Vector3(xPosition, yPosition, 0),
                Quaternion.identity);
            temp.GetComponent<MainToken>().faceIndex = faceIndexes[shuffleNum];
            faceIndexes.Remove(faceIndexes[shuffleNum]);

            xPosition = xPosition + 4.0f;
            if (i == 4)
            {
                xPosition = -8f;
                yPosition = -1.5f;
            }
        }
    }

    private void Update()
    {
        if (start == true)
        {
            timer += Time.deltaTime;
            temp = (float)Math.Round(timer);
            score.text = temp.ToString();
        }
          
        if (count == 5)
        {
            start = false;
        }

    }

    private MainToken FirstCard;
    private MainToken SecondCard;

    public void imageOpened(MainToken card)
    {
        
        if (FirstCard == null)
        {
            if (!start)
                start = true;
            FirstCard = card;
        }
        else
        {  
            SecondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    public IEnumerator CheckMatch()
    {
        if (FirstCard.index == SecondCard.index)
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

    public void Reset()
    {
        SceneManager.LoadScene(2);
    }

    private void Awake()
    {
        token = GameObject.Find("Token");
    }
}
