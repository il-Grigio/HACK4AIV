using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : Grigios.Singleton<TutorialManager> 
{
    public enum TutorialTriggers { start, destructionRoom, tab, craftingRoom, deliver, none}

    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] GenericCollider destructionRoom;
    [SerializeField] UIAnimationTrigger uIAnimationTrigger;
    [SerializeField] GenericCollider craftingRoom;
    bool deliverRecepie = false;
    #region text
    string welcome = "Welcome <insert unit ID here> to Dump Inc., where certified trash becomes something new and beautiful, just like you! ";
    string welcome2 = "I am here to guide you through your new <and permanent> assignment and ensure you achieve a satisfying level of proficiency. " +
        "Should you prove youself unable to reach the company standards, worry not! You'll still be a <VALUED COMPONENT> of our manufactoring department!";
    string welcome3 = "To begin your orientation tour, let's head to the room on the left.";
    
    //Wait until player is in room
    string goToDestructionRoom = "Welcome to the Disassembly room, where incoming components get processed into base materials. " +
        "In order to reduce waste of materials you'll only have access to a limited amount at any given time. " +
        "If you feel you don't have use for one, You can return it using one of the Item Recycling Bins throught the rooms. Do you wish to hear more about the Disassembly room?";
    string goToDestructionRoom2 = "<Company Policy dictates all non verbal responses be intepreted as affirmations> As you wish! " +
        "Robots of the <insert unit type here> line can produce large quantities of high quality circuitry! Isn't it exciting?";
    string goToDestrucitonRoom3 = "In this room you'll have access to various disassembly stations. Each machine specializes in the production of a single material. " +
        "Please keep in mind that some are currently inactive and will be activated once the particular material is needed. As you can see," +
        " limited access to machinery results in lower levels of confusions, achieving sufficient ease of use for ALL workers.";

    string openTheTab = "To further aid you in your efforts, each succesfull interaction with the station will be recorded into an easily accessible list to the left of your vision.";

    //Open the tab
    string orders = "Speaking of heads-up display, by now you'll have noticed the incoming requests at the bottom of your vision. " +
        "You are required to complete and deliver them all before the end of your Work Shift. In a sign of appreciation to efficient workers, the company extends shift duration for each delivered product.";


    string goToCraftingRoom = "Once you have all the materials needed for a Request, you'll need to bring them to the Processing Station. Please proceed to relocate to the room at the far right!";

    //wait until player is in room
    string goToCraftingRoom2 = "The processing station will take any combination of materials and turn them into new, shining products! " +
        "<WARNING- not all combinations result into viable products. Waste of the company materials is frowned upon>";

    string deliver = "Once your new product has been created, you will need to deliver it to the conveyor belt at the center of your complex.";

    //wait for deliver
    string workShift = "<Please note that, in order to achieve desired worker proficiency, the company has grouped together Working Shifts ONE to FIVE. " +
        "Completion of current shift will automatically start the next one. Dump Inc. is not liable for any kind of accident during work hours>";

    string goodbye = "We hope you'll have a GREAT time here at Dump Inc.! Good luck, <insert unit ID here>";
    #endregion

    List<StringsAndTimes> myLists = new List<StringsAndTimes>();
    public int currentIndex = -1;

    float currentTime = 0;
    private void Awake() {
        currentTime = 0;
        currentIndex = 0;
        deliverRecepie = false;

        myLists.Add(new StringsAndTimes("", 0, true, TutorialTriggers.start));

        myLists.Add(new StringsAndTimes(welcome, 8.5f));    
        myLists.Add(new StringsAndTimes(welcome2, 14.5f));    
        myLists.Add(new StringsAndTimes(welcome3, 3.5f, true, TutorialTriggers.destructionRoom));

        myLists.Add(new StringsAndTimes(goToDestructionRoom, 18.5f));    
        myLists.Add(new StringsAndTimes(goToDestructionRoom2, 13.5f));    
        myLists.Add(new StringsAndTimes(goToDestrucitonRoom3, 18.5f));    
        myLists.Add(new StringsAndTimes(openTheTab, 6.5f, true, TutorialTriggers.tab));    

        myLists.Add(new StringsAndTimes(orders, 14.5f));    
        myLists.Add(new StringsAndTimes(goToCraftingRoom, 8.5f, true, TutorialTriggers.craftingRoom));   
        
        myLists.Add(new StringsAndTimes(goToCraftingRoom2, 11.5f));    
        myLists.Add(new StringsAndTimes(deliver, 5.5f, true, TutorialTriggers.deliver));    

        myLists.Add(new StringsAndTimes(workShift, 14.5f));    
        myLists.Add(new StringsAndTimes(goodbye, 6.5f));    

    }
    private void Update() {
        currentTime += Time.deltaTime;
        if(currentIndex < myLists.Count && currentTime >= myLists[currentIndex].t) {
            if (!myLists[currentIndex].shouldStop) {
                NextIndex();
            }
            else {
                switch (myLists[currentIndex].tutorialTriggers) {
                    case TutorialTriggers.start:
                        if(ClientOrderMGR.Instance.CurrentTimePercentage < 1) {
                            AudioManager.instance.InizializeSpeechEvent(FMODEvents.instance.speechEvent);
                            NextIndex();
                        }
                        break;
                    case TutorialTriggers.destructionRoom:
                        if (destructionRoom.isColliding) {
                            NextIndex();
                        }
                        break;
                    case TutorialTriggers.tab:
                        if (uIAnimationTrigger.status || deliverRecepie) {
                            NextIndex();
                        }
                        break;
                    case TutorialTriggers.craftingRoom:
                        if (craftingRoom.isColliding) {
                            NextIndex();
                        }
                        break;
                    case TutorialTriggers.deliver:
                        if (deliverRecepie) {
                            NextIndex();
                        }
                        break;
                    case TutorialTriggers.none:
                        break;
                    default:
                        break;
                }
            }
        }
        if(currentIndex >= myLists.Count) {
            textMeshProUGUI.text = "";
            return;
        }
        textMeshProUGUI.text = myLists[currentIndex].s;
    }
    private void NextIndex() {
        currentIndex++;
        currentTime = 0;
        AudioManager.instance.SetSpeechParameter("Speech", currentIndex);
    }

    public void DeliverARecepie() {
        deliverRecepie = true;
    }

    class StringsAndTimes {
        public string s;
        public float t;
        public bool shouldStop;
        public TutorialTriggers tutorialTriggers;
        public StringsAndTimes(string _s, float _t, bool _shouldStop = false, TutorialTriggers tutorialTriggers = TutorialTriggers.none) {
            s = _s; t = _t; shouldStop = _shouldStop;
            this.tutorialTriggers = tutorialTriggers;
        }
    }

    

}
