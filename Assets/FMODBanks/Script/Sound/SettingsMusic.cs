using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMusic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textValueMaster;
    [SerializeField] TextMeshProUGUI textValueMusic;
    [SerializeField] TextMeshProUGUI textValueAmbience;
    [SerializeField] TextMeshProUGUI textValueSFX;
    private void Update()
    {
        textValueMaster.text = AudioManager.instance.masterVolume.ToString();
        textValueMusic.text = AudioManager.instance.musicVolume.ToString();
        textValueSFX.text = AudioManager.instance.SFXVolume.ToString();
        textValueAmbience.text = AudioManager.instance.ambienceVolume.ToString();
    }
    public void UpMusic(int busToTake)
    {
        switch (busToTake)
        {
            case (1):
                {
                    if (AudioManager.instance.masterVolume >= 10)
                    {
                        AudioManager.instance.masterVolume = 10;
                    }
                    else
                    {
                        AudioManager.instance.masterVolume++;
                    }
                    break;
                }
            case (2):
                {
                    if (AudioManager.instance.musicVolume >= 10)
                    {
                        AudioManager.instance.musicVolume = 10;
                    }
                    else
                    {
                        AudioManager.instance.musicVolume++;
                    }
                    break;
                }
            case (3):
                {
                    if (AudioManager.instance.SFXVolume >= 10)
                    {
                        AudioManager.instance.SFXVolume = 10;
                    }
                    else
                    {
                        AudioManager.instance.SFXVolume++;
                    }
                    break;
                }
            case (4):
                {
                    if (AudioManager.instance.ambienceVolume >= 10)
                    {
                        AudioManager.instance.ambienceVolume = 10;
                    }
                    else
                    {
                        AudioManager.instance.ambienceVolume++;
                    }
                    break;
                }
            default:
                break;
        }
    }

    public void DownMusic(int busToTake)
    {
        switch (busToTake)
        {
            case (1):
                {
                    if (AudioManager.instance.masterVolume <= 0)
                    {
                        AudioManager.instance.masterVolume = 0;
                    }
                    else
                    {
                        AudioManager.instance.masterVolume--;
                    }
                    break;
                }
            case (2):
                {
                    if (AudioManager.instance.musicVolume <= 0)
                    {
                        AudioManager.instance.musicVolume = 0;
                    }
                    else
                    {
                        AudioManager.instance.musicVolume--;
                    }
                    break;
                }
            case (3):
                {
                    if (AudioManager.instance.SFXVolume <= 0)
                    {
                        AudioManager.instance.SFXVolume = 0;
                    }
                    else
                    {
                        AudioManager.instance.SFXVolume--;
                    }
                    break;
                }
            case (4):
                {
                    if (AudioManager.instance.ambienceVolume <= 0)
                    {
                        AudioManager.instance.ambienceVolume = 0;
                    }
                    else
                    {
                        AudioManager.instance.ambienceVolume--;
                    }
                    break;
                }
            default:
                break;
        }
    }
}