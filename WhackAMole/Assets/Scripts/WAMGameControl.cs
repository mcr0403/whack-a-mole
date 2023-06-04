using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WAMGameControl : MonoBehaviour
{
    public List<Mole> moles;

    public GameObject imageFrame;
    public GameObject pauseScreen;

    public TMP_Text timeText;
    public TMP_Text scoreText;
    public HashSet<Mole> currentMoles = new HashSet<Mole>();
    public UnityEngine.UI.Image image;

    private float startingTime = 10f;
    private float time;
    private int score;
    private bool playing = false;

    public Data[] vocabularies;
    public List<Data> lesson;
    public string lessonName ;
    public int indexImage = 0;
    public List<Data> listWordShow;
    public int lenghtList = 2;
    public GameObject hammer;
    public GameObject vocab;
    public GameObject star;
    public GameObject list;
    public string vocabName;
    private int stars ;

    public AudioSource backgroundMusic;
    public AudioSource getPointSound;
    public AudioSource missedSound;
    public AudioSource gameOverSound;

    // Start is called before the first frame update
    void Start()
    {

        foreach (Data v in vocabularies)
        {
            if(v.session == lessonName)
            {
                lesson.Add(v);
            }
        }

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            //set time
            int indexGetWord = 0;
            time -= Time.deltaTime;
            timeText.text = Mathf.FloorToInt(time%60).ToString();

            if (time <= 0)
            {
                timeText.text = "0";
                GameOver();
            }

            //initialize queue of random mole
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

            //set mole index
            if (currentMoles.Count <= (score / 5))
            {
                int randomMole = Random.Range(0, moles.Count);

                if (!currentMoles.Contains(moles[randomMole]))
                {
                    currentMoles.Add(moles[randomMole]);
                    moles[randomMole].AtCall(score / 5);
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
        pauseScreen.SetActive(false);
        timeText.enabled = true;
        scoreText.enabled = true;
        listWordShow.Clear();
        int random = Random.Range(0, lesson.Count);
        listWordShow.Add(lesson[random]);
        hammer.SetActive(true);
        imageFrame.SetActive(true);

        for (int i = 0; i < moles.Count; i++)
        {
            moles[i].SetIndex(i);
        }
        currentMoles.Clear();
        time = startingTime;
        score = 0;
        scoreText.text = "0";
        playing = true;
        backgroundMusic.Play();
    }

    //show overgame screen and stop action in game
    public void GameOver()
    {
        foreach (Mole mole in moles)
        {
            mole.StopGame();
        }
        pauseScreen.SetActive(true) ;
        playing = false;
        hammer.SetActive(false);
        backgroundMusic.Stop();
        gameOverSound.Play();

        if (score > 0 && score <= 15)
        {
            stars = 1;
        }
        if (score > 15 && score <= 19)
        {
            stars = 2;
        }
        if (score > 19)
        {
            stars = 3;
        }

        for (int i = 0; i < stars; i++)
        {
            var starSpawn = Instantiate(star, GameObject.Find("Canvas/Panel/GameOver/Score").transform);
        }
    }

    // smash correc, scrore +1, time +1, disappear that mole
    public void AddScore(int moleIndex)
    {
        AddVocab();
        getPointSound.Play();
        score++;
        scoreText.text = score.ToString();
        time++;
        currentMoles.Remove(moles[moleIndex]);
    }

    //if hit wrong mole, score -2 and disappear that mole
    public void Missed(int moleIndex, bool isMole)
    {
        if (isMole)
        {
            time -= 2;
            missedSound.Play();
        }

        currentMoles.Remove(moles[moleIndex]);
    }

    public void ReStartButton()
    {
        SceneManager.LoadScene("WhackAMole");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("WAMMenu");
    }

    //show vocab after play
    public void AddVocab()
    {
        var vocabSpawn = Instantiate(vocab, list.transform);
        var imageVocab = vocabSpawn.transform.GetChild(1).gameObject;
        var textVocab = vocabSpawn.transform.GetChild(2).gameObject;

        imageVocab.GetComponent<UnityEngine.UI.Image>().sprite = image.sprite;
        textVocab.GetComponent<TMP_Text>().text = vocabName;
    }
}


//Reference link :"https://github.com/Firnox/Whack-A-Mole"