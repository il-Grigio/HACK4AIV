using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    [SerializeField] GameObject loseGameScreen;
    [SerializeField] GameObject thanksForPlayingObj;
    [SerializeField] FadeEffect fade;
    bool gameFinish = false;
    float timer = 5;
    float currentTimer = 0;
    float speed = 1;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameFinish)
        {
            currentTimer += speed * Time.deltaTime;
            if (currentTimer >= timer)
            {
                currentTimer = 0;
                SceneManager.LoadScene("AntonioScena");
            }
        }
       
    }

    public void EndLoseGame() 
    {
        gameFinish = true;
        fade.gameObject.SetActive(true);
        fade.FadeIn();
        loseGameScreen.SetActive(true);
        thanksForPlayingObj.SetActive(true);
        AudioManager.instance.StopMusic();
    }
}
