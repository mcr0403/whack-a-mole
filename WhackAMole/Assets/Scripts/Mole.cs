using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public GameObject mole;
    public GameObject moleHit;
    public WAMGameControl gameControl;

    private Vector3 startPosition = new Vector3(0f, -2.9f, 0f);
    private Vector3 endPosition = Vector3.zero;

    public float duration = 1f;
    public float showDuration = .5f;
    private bool oneHit = true;
    public float random;
    private int moleIndex = 0;

    public TMP_Text word;
    public string vocabulary;
    public int indexWord;
    public int indexImage;

    private void Update()
    {
        word.text = vocabulary;
       
    }

    private IEnumerator ShowMole(Vector3 start, Vector3 end)
    {

        transform.localPosition = start;
        //show mole
        float time = 0f;
        while (time < showDuration)
        {
            transform.localPosition = Vector3.Lerp(start, end, time / showDuration);
            time += Time.deltaTime;
            yield return null;
        }
                                                       
        transform.localPosition = end;

        yield return new WaitForSeconds(duration);

        //hide mole
        time = 0f;
        while (time < showDuration)
        {
            transform.localPosition = Vector3.Lerp(end, start, time / showDuration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = start;
        gameControl.Missed(moleIndex, false);
    }
    private void OnMouseDown()
    {
        //compare mole and image
        if (oneHit)
        {   
            if (CheckResult())
            {
                gameControl.vocabName = word.text;
                gameControl.listWordShow.RemoveAt(indexWord);
                gameControl.AddScore(moleIndex);

            } else
            {
                gameControl.Missed(moleIndex,false);
            }
            StopAllCoroutines();
            moleHit.SetActive(true);
            mole.SetActive(false);
            oneHit = false;
            StartCoroutine(Hide());
        } else
        { 
            gameControl.Missed(moleIndex, false);
        }


    }

    //hide mole if mole is smashed
    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(.25f);

        if (!oneHit)
            transform.localPosition = startPosition;

    }

    //what next mole in this hole
    private void NextMole()
    {
        mole.SetActive(true);
        moleHit.SetActive(false);
        word.enabled = true;

        oneHit = true;
    }

    //increase level when score increase
    private void UpLevel(int level)
    {   

        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
        duration = UnityEngine.Random.Range(durationMin, durationMax);
    }

    //set mole when controller call
    public void AtCall(int level)
    {
        UpLevel(level);
        NextMole();
        StartCoroutine(ShowMole(startPosition, endPosition));
    }

    //Check image and text match
    public bool CheckResult()
    {
        indexImage = gameControl.indexImage;
        if (indexImage == indexWord)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void SetIndex(int index)
    {
        moleIndex = index;
    }

    public void StopGame()
    {
        oneHit = false;
        StopAllCoroutines();
    }

}
