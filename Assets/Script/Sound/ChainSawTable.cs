using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;


public class ChainSawTable : MonoBehaviour
{
    [SerializeField]float timeToReach;
    [SerializeField]float speed;
    [SerializeField]float timer = 0;
    EventInstance sound;
    void Start()
    {
        
         sound = AudioManager.instance.CreateInstance(FMODEvents.instance.ChainSawTable);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R) && timer <= timeToReach) 
        {
            sound.start();
            timer += speed * Time.deltaTime;
        }
        else 
        {
            sound.stop(STOP_MODE.IMMEDIATE);

        }
    }
}
