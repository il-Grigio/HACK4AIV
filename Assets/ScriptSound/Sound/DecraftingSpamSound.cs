using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class DecraftingSpamSound : MonoBehaviour
{

    DecraftingSpamMachine machine;
    [SerializeField] EventReference soundToPlay;
    int currentSoundPlayed = 0;
    void Start()
    {
        machine = GetComponent<DecraftingSpamMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSoundPlayed < machine.currentInteractions) 
        {
            AudioManager.instance.PlayOneShot(soundToPlay, this.transform.position);
            currentSoundPlayed++;
        }
    }
}
