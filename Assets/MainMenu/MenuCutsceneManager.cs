using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MenuCutsceneManager : MonoBehaviour {
    [SerializeField] private Animator robotController;
    [SerializeField] private GameObject[] GOToEnable;
    [SerializeField] private GameObject[] GOToDisable;

    public void StartRobotWalking() {
        robotController.SetTrigger("Walk");
    }

    public void StopRobotWalking() {
        robotController.SetTrigger("Stop");
    }

    public void DisableMenuCameraAndStartGame() {
        foreach (GameObject go in GOToEnable) { go.SetActive(true); }
        foreach (GameObject go in GOToDisable) { go.SetActive(false); }
    }
}
