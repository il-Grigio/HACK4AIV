using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Grigios.Singleton<TutorialManager> 
{
    string welcome = "Welcome to Dump Incorporated. In this tutorial, you will learn the ropes of being a skilled robot.";
    string goToDestructionRoom = "Let's head LEFT to the room where most of your work will take place.";
    string decraftingExplenation = 
        "Here is where you'll find the tools of your job. " +
        "Once you provide the base material," +
        " they will return raw materials to you. By following customer recipes and combining the raw materials, you'll be able to create great things. Use the interact button to interact with them.";

}
