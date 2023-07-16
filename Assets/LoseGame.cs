using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseGame : MonoBehaviour
{
    [SerializeField] GameObject loseGameScreen;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndLoseGame() 
    {
        loseGameScreen.SetActive(true);
        AudioManager.instance.StopMusic();
    }
}
