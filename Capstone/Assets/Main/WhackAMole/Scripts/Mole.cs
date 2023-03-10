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
    private CapsuleCollider capsCollider;
    public WAMGameControl gameControl;
    public GameObject bomb;

    private Vector3 startPosition = new Vector3(0f, -2.9f, 0f);
    private Vector3 endPosition = Vector3.zero;

    public float duration = 1f;
    public float showDuration = .5f;
    private bool oneHit = true;
    public float random;
    private int moleIndex = 0;
    public float bombRate = 0.2f;

    public enum MoleType
    {
        Standard,
        Bomb
    }

    private MoleType moleType;

    public TMP_Text word;
    private static System.Random rdm = new System.Random();
    public string vocabulary;
    public int indexWord;
    public int indexImage; 

    private void Update()
    {
        word.text = vocabulary;
    }

    private IEnumerator ShowHide(Vector3 start, Vector3 end)
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
        if (oneHit)
        {
            oneHit = false;
            gameControl.Missed(moleIndex, moleType != MoleType.Bomb);
        }
    }
    private void OnMouseDown()
    {
        if (oneHit)
        {    
            if (CheckResult())
            {
                gameControl.listWordShow.RemoveAt(indexWord);
                gameControl.AddScore(moleIndex);

            } else
            {
                gameControl.Missed(moleIndex, moleType != MoleType.Bomb);
            }
            StopAllCoroutines();
            StartCoroutine(QuickHide());
            moleHit.active = true;
            mole.active = false;
            oneHit = false;
        }
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(.25f);

        if (!oneHit)
            Hide();

    }

    public void Hide()
    {
        transform.localPosition = startPosition;
    }


    private void CreateNext()
    {
        random = UnityEngine.Random.Range(0f, 1f);
        if (random < bombRate)
        {
            moleType = MoleType.Bomb;
        }
        else
        {
            moleHit.active = false;
            moleType = MoleType.Standard;
            mole.active = true;
        }

        
        oneHit = true;
    }

    private void SetLevel(int level)
    {
        //increase bomb rate at level 10
        bombRate = Mathf.Min(level * 0.05f, 0.5f);


        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
        duration = UnityEngine.Random.Range(durationMin, durationMax);
    }

    public void Activate(int level)
    {
        SetLevel(0);
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

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

    private void Awake()
    {
 
    }
}
