using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class SoundMachine : MonoBehaviour
{
    bool soundIsPlaying = false;
    StudioEventEmitter emitter;
    DecraftingMachine machine;
    [SerializeField] EventReference soundToPlay;
    void Start()
    {
        machine = GetComponent<DecraftingMachine>();
        emitter = AudioManager.instance.InitializeEventEmitter(soundToPlay, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.isStarted && !soundIsPlaying)
        {
            emitter.Play();
            soundIsPlaying = true;
        }
        else if(machine.isStarted == false)
        {
            soundIsPlaying = false ;
        }
        

    }


}
