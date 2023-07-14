using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "new Recepie", menuName = "Recepie")]
public class Recepie : ScriptableObject {
    public float timeToFinishRecepie = 60;
    [HideInInspector] public float currentTime;
    public float timeBonusOnComplete;
    public float timeLostOnIncomplete;
    public IngredientScriptable recepie;
}
