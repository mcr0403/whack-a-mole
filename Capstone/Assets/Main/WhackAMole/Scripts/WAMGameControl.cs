using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WAMGameControl : MonoBehaviour
{
    public List<Mole> moles;

    public GameObject playButton;
    public GameObject outOfTime;
    public GameObject boom;
    public TMP_Text timeText;
    public TMP_Text scoreText;
    private HashSet<Mole> currentMoles = new HashSet<Mole>();
    public UnityEngine.UI.Image image;

    private float startingTime = 30f;
    private float timeRemaining;
    private int score;
    private bool playing = false;

    public Vocabulary[] vocabularies;
    public List<Vocabulary> lesson;
    public string lessonName ;
    private string word;
    public int indexImage = 0;
    public List<Vocabulary> listWordShow;
    public int lenghtList = 2;
 

    // Start is called before the first frame update
    void Start()
    {
        foreach (Vocabulary v in vocabularies)
        {
            if(v.lessonName == lessonName)
            {
                lesson.Add(v);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            int indexGetWord = 0;
            timeRemaining -= Time.deltaTime;
            timeText.text = timeRemaining.ToString();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver(0);
            }

            if (listWordShow.Count < lenghtList)
            {
                int randomIndex = Random.Range(0, lesson.Count);
                
                if (!listWordShow.Contains(lesson[randomIndex]))
                {
                    listWordShow.Add(lesson[randomIndex]);
                }
            }

            foreach (var mole in currentMoles)
            {
                if (mole.indexWord == indexImage)
                {
                    indexGetWord = 1;
                }
            }

            if (currentMoles.Count <= (score / 5))
            {
                int randomMole = Random.Range(0, moles.Count);

                if (!currentMoles.Contains(moles[randomMole]))
                {
                    currentMoles.Add(moles[randomMole]);
                    moles[randomMole].Activate(score / 5);

                    moles[randomMole].indexWord = indexGetWord;
                    moles[randomMole].vocabulary = listWordShow[indexGetWord].name;
                    if (indexGetWord != indexImage)
                    {
                        listWordShow.RemoveAt(indexGetWord);
                    }   
                }
            }

            image.sprite = listWordShow[indexImage].image;
        }
    }

    public void StartGame()
    {
        playButton.SetActive(false);
        timeText.enabled = true;
        scoreText.enabled = true;
        outOfTime.SetActive(false);
        boom.SetActive(false);
        image.color = Color.white;
        listWordShow.Clear();
        int random = Random.Range(0, lesson.Count);
        listWordShow.Add(lesson[random]);

        for (int i = 0; i < moles.Count; i++)
        {
            moles[i].Hide();
            moles[i].SetIndex(i);
        }
        currentMoles.Clear();
        timeRemaining = startingTime;
        score = 0;
        scoreText.text = "0";
        playing = true;
    }

    public void GameOver(int type)
    {
        if (type == 0)
        {
            outOfTime.SetActive(true);
        }
        else
        {
            boom.SetActive(true);
        }

        foreach (Mole mole in moles)
        {
            mole.StopGame();
        }
        playing = false;
        playButton.SetActive(true);
    }

    public void AddScore(int moleIndex)
    {
        score++;
        scoreText.text = score.ToString();
        timeRemaining++;
        currentMoles.Remove(moles[moleIndex]);
    }

    public void Missed(int moleIndex, bool isMole)
    {
        if (isMole)
        {
            timeRemaining -= 2;
        }

        currentMoles.Remove(moles[moleIndex]);
    }


}
