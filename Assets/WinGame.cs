using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField] GameObject winGameScreen;
    [SerializeField] GameObject thanksForPlayingObj;
    [SerializeField] FadeEffect fade;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndWinGame()
    {
        fade.FadeIn();
        winGameScreen.SetActive(true);
        thanksForPlayingObj.SetActive(true);
        AudioManager.instance.StopMusic();
    }
}
