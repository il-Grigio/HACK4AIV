using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MenuLabels {
    NewGame,
    Settings,
    Credits,
    Quit
}
public class MenuManager : MonoBehaviour
{
    [SerializeField] private LayerMask rayMask;
    [SerializeField] private MenuButton[] buttons;
    private int selectedButton;
    private bool hasChangedSelection;

    private string menuAxis = "MenuSelection";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        //Joypad selection
        if (!hasChangedSelection) {
            selectedButton += (int)Input.GetAxisRaw(menuAxis);
            if((int)Input.GetAxisRaw(menuAxis) != 0) {
                hasChangedSelection = true;
            }
            selectedButton = Mathf.Clamp(selectedButton, (int)MenuLabels.NewGame, (int)MenuLabels.Quit);
        }

        if (Mathf.RoundToInt(Input.GetAxisRaw(menuAxis)) == 0) {
            hasChangedSelection = false;
        }

        //Cursor selection (Overwrites Joypad selection)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f, rayMask)) {
            if(hit.transform.name == "NewGame") {
                selectedButton = (int)MenuLabels.NewGame;
            }
            if (hit.transform.name == "Settings") {
                selectedButton = (int)MenuLabels.Settings;
            }
            if (hit.transform.name == "Credits") {
                selectedButton = (int)MenuLabels.Credits;
            }
            if (hit.transform.name == "Quit") {
                selectedButton = (int)MenuLabels.Quit;
            }

            if (Input.GetMouseButtonDown(0)) {
                if (buttons[selectedButton]) {
                    buttons[selectedButton].ExecuteButton();
                }
            }
        }
    }

    private void ChangeSelector() {

    }
}
