using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenScript : MonoBehaviour
{
    private bool fS = false;

    public void changeFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        fS = !fS;
        Debug.Log("Funziona Stronzo");
    }

}
