using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    [SerializeField] GameObject winGameScreen;
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
            currentTimer += speed*Time.deltaTime;
            if (currentTimer >= timer)
            {
                currentTimer = 0;
                SceneManager.LoadScene("AntonioScena");
            }
        }
    }
    public void EndWinGame()
    {
        gameFinish = true;
        fade.gameObject.SetActive(true);
        fade.FadeIn();
        winGameScreen.SetActive(true);
        thanksForPlayingObj.SetActive(true);
        AudioManager.instance.StopMusic();
    }
}
