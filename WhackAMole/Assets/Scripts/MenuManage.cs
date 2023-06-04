using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{

    public GameObject mole;
    public GameObject moleHit;
    public AudioSource bonk;
    public AudioSource bgMusic;
    // Start is called before the first frame update
    void Start()
    {
        bgMusic.Play();
    }

    // Update is called once per frame


    public void PlayButton()
    {
        SceneManager.LoadScene("WhackAMole");
        bgMusic.Stop();
    }

    public void ExitButton()
    {
        bgMusic.Stop();
        Application.Quit();
    }

    private void OnMouseDown()
    {
        bonk.Play();
        mole.SetActive(false);
        moleHit.SetActive(true);
        StartCoroutine(MouseHit());
    }

    private IEnumerator MouseHit()
    {
        yield return new WaitForSeconds(0.25f);
        moleHit.SetActive(false);
        mole.SetActive(true);
    }
}
