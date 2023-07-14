using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class CraftingMachinSound : MonoBehaviour
{
    bool soundIsPlaying = false;
    StudioEventEmitter emitter;
    CraftingMachine machine;
    [SerializeField] EventReference soundToPlay;
    // Start is called before the first frame update
    void Start()
    {
        machine = GetComponent<CraftingMachine>();
        emitter = AudioManager.instance.InitializeEventEmitter(soundToPlay, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.itemToCraft !=null && !soundIsPlaying)
        {
            emitter.Play();
            soundIsPlaying = true;
        }
        else if (machine.itemToCraft == false)
        {
            soundIsPlaying = false;
        }
    }
}
