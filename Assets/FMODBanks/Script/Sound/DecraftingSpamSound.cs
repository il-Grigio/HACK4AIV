using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class DecraftingSpamSound : MonoBehaviour
{

    StudioEventEmitter emitter;
    DecraftingSpamMachine machine;
    [SerializeField] EventReference soundToPlay;
    int currentSoundPlayed;
    void Start()
    {
        machine = GetComponent<DecraftingSpamMachine>();
        emitter = AudioManager.instance.InitializeEventEmitter(soundToPlay, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSoundPlayed < machine.currentInteractions) 
        {
            emitter.Play();
            currentSoundPlayed++;
        }
        else if(currentSoundPlayed >= machine.currentInteractions) 
        {

            emitter.Stop();
        }
    }
}
