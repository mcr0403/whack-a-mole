using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestionControl : MonoBehaviour
{
    public List<Vocabulary> vocabList;
    List<Vocabulary> lessonVocab = new List<Vocabulary>();

    public Image QuestionImage;
    public Text QuestionText;
    public Text AnswerText1;
    public Text AnswerText2;
    public Text AnswerText3;
    public Text AnswerText4;
    public string correctAnswer;
    public void Start()
    {
        foreach (Vocabulary v in vocabList)
        {
            if (v && v.lessonName == "Animal")
            {
                lessonVocab.Add(v);
            }
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            generateQuestion();
        }
    }
    void generateQuestion()
    {
        QuestionText.text = "Which animal is this?";
        AnswerText1.text = "";
        AnswerText2.text = "";
        AnswerText3.text = "";
        AnswerText4.text = "";
        int a = Random.Range(0, lessonVocab.Count - 1);
        QuestionImage.GetComponent<Image>().sprite = lessonVocab[a].image;
        int b = Random.Range(1, 4);
        List<Vocabulary> temp = new List<Vocabulary>(lessonVocab);
        temp.Remove(lessonVocab[a]);
        switch (b)
        {
            case 1:
                AnswerText1.text = lessonVocab[a].word;
                correctAnswer = AnswerText1.name;
                break;
            case 2:
                AnswerText2.text = lessonVocab[a].word;
                correctAnswer = AnswerText2.name;
                break;
            case 3:
                AnswerText3.text = lessonVocab[a].word;
                correctAnswer = AnswerText3.name;
                break;
            case 4:
                AnswerText4.text = lessonVocab[a].word;
                correctAnswer = AnswerText4.name;
                break;
        }
        if (AnswerText1.text == "")
        {
            a = Random.Range(0, temp.Count - 1);
            AnswerText1.text = temp[a].word;
            temp.Remove(temp[a]);
        }
        if (AnswerText2.text == "")
        {
            a = Random.Range(0, temp.Count - 1);
            AnswerText2.text = temp[a].word;
            temp.Remove(temp[a]);
        }
        if (AnswerText3.text == "")
        {
            a = Random.Range(0, temp.Count - 1);
            AnswerText3.text = temp[a].word;
            temp.Remove(temp[a]);
        }
        if (AnswerText4.text == "")
        {
            a = Random.Range(0, temp.Count - 1);
            AnswerText4.text = temp[a].word;
            temp.Remove(temp[a]);
        }
    }

    public void Answer()
    {
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (button.transform.GetChild(0).gameObject.name == correctAnswer)
        {
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            button.GetComponent<Image>().color = Color.red;

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            button.GetComponent<Image>().color = Color.white;
        }
    }
}

//.transform.GetChild(0).gameObject
//button.GetComponent<Image>().color = Color.white;