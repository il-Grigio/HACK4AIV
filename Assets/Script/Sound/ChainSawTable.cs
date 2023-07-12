using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class ChainSawTable : MonoBehaviour
{
    [SerializeField]float timeToReach;
    [SerializeField]float speed;
    [SerializeField]float timer = 0;
    bool soundIsPlaying = false;
    StudioEventEmitter emitter;
    void Start()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.ChainSawTable, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R) && timer <= timeToReach) 
        {
            timer += speed * Time.deltaTime;
            if(soundIsPlaying == false) 
            {
                emitter.Play();
                soundIsPlaying = true;
            }
        }
        else 
        {
            emitter.Stop();
            if(timer < timeToReach) 
            {
                timer = 0;
            }
            soundIsPlaying = false;
        }

    }


}
