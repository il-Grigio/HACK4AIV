using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseGame : MonoBehaviour
{
    [SerializeField] GameObject loseGameScreen;
    [SerializeField] GameObject thanksForPlayingObj;
    [SerializeField] FadeEffect fade;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndLoseGame() 
    {
        fade.gameObject.SetActive(true);
        fade.FadeIn();
        loseGameScreen.SetActive(true);
        thanksForPlayingObj.SetActive(true);
        AudioManager.instance.StopMusic();
    }
}
