using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 10)]
    public int masterVolume = 10;
    [Range(0, 10)]
    public int musicVolume = 10;
    [Range(0, 10)]
    public int ambienceVolume = 10;
    [Range(0, 10)]
    public int SFXVolume = 10;

    public static AudioManager instance { get; private set; }

    Bus masterBus;
    Bus musicBus;
    Bus ambienceBus;
    Bus SFXBus;

    private EventInstance ambienceEventInstance;
    private EventInstance musicMenuInstance;


    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("AudioManager already exist in the scene");
        }
        instance = this;
        masterBus = RuntimeManager.GetBus("Bus:/");
        musicBus = RuntimeManager.GetBus("Bus:/MUSICBus");
        ambienceBus = RuntimeManager.GetBus("Bus:/AMBIENCEBus");
        SFXBus = RuntimeManager.GetBus("Bus:/SFXBus");
    }

    private void Start()
    {
        InizializeAmbience(FMODEvents.instance.ambienceSound);
        InizializeMusicMenu(FMODEvents.instance.musicMenu);
    }

    private void InizializeAmbience(EventReference ambienceEvent)
    {
        ambienceEventInstance = CreateInstance(ambienceEvent);
        ambienceEventInstance.start();
    }


    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        return emitter;
    }

    private void InizializeMusicMenu(EventReference menuMusic)
    {
        musicMenuInstance = CreateInstance(menuMusic);
        musicMenuInstance.start();
    }

    public EventInstance CreateInstance(EventReference eventeReference)
    {
        EventInstance myEvent = RuntimeManager.CreateInstance(eventeReference);
        return myEvent;
    }


    // Update is called once per frame
    void Update()
    {
        masterBus.setVolume(masterVolume / 10f);
        musicBus.setVolume(musicVolume / 10f);
        ambienceBus.setVolume(ambienceVolume / 10f);
        SFXBus.setVolume(SFXVolume / 10f);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);

    }

    public void SetMusicParameter(string nameParameter, float parameterValue) 
    {
        musicMenuInstance.setParameterByName(nameParameter, parameterValue);
    }
    public void StopMusic()
    {
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
