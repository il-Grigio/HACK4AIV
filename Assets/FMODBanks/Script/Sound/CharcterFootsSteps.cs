using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using FMOD.Studio;

public class CharcterFootsSteps : MonoBehaviour
{
    CharacterAbility controller;
    private EventInstance footsSteps;
    void Start()
    {
        controller = GetComponent<CharacterAbility>();
        footsSteps = AudioManager.instance.CreateInstance(FMODEvents.instance.footsStep);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSound();
    }

    void UpdateSound()
    {
        if (controller._movement.CurrentState == CharacterStates.MovementStates.Walking)
        {
            PLAYBACK_STATE playbackState;
            footsSteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                footsSteps.start();
            }
        }
        else if (controller._movement.CurrentState != CharacterStates.MovementStates.Walking)
        {
            footsSteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}