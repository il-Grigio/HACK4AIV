using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class DecraftingBurnSound : MonoBehaviour
{
    StudioEventEmitter emitter;
    DecraftingBurnMachine machine;
    [SerializeField] EventReference soundToPlay;
    bool isPlaying;

    void Start()
    {
        machine = GetComponent<DecraftingBurnMachine>();
        emitter = AudioManager.instance.InitializeEventEmitter(soundToPlay, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(machine.isStarted && !isPlaying) 
        {
              emitter.Play();
              isPlaying = true;
        }
        if (machine.isBurning) 
        {
            emitter.SetParameter("IsBurning", 1);
        }
        if (!machine.isStarted && !machine.isBurning) 
        {
            isPlaying = false;
            emitter.Stop();
        }
    }
}
