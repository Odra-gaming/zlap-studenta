using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator transition;

    public float waitTime = 1f;

    public void PlayWroclaw()
    {

        StartCoroutine(TransitionNext(1));
    }

    public void PlayLublin()
    {

        StartCoroutine(TransitionNext(2));
    }

    public void PlayKrakow()
    {

        StartCoroutine(TransitionNext(3));
    }

    public void PlayPoznan()
    {

        StartCoroutine(TransitionNext(4));
    }

    public void PlayWarszawa()
    {

        StartCoroutine(TransitionNext(5));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        Application.Quit();
    }

    IEnumerator TransitionNext(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(levelIndex);
    }
}
