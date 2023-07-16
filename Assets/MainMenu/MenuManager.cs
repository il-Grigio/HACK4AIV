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
    [SerializeField] private Transform selector;
    private Vector3 selectorOffset;
    private int selectedButton;
    private bool hasChangedSelection;

    private string menuAxis = "MenuSelection";
    private string menuSelectionAxis = "MenuConfirm";

    private void Awake() {
        selectorOffset = selector.localPosition;
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

        if (Input.GetButtonDown(menuSelectionAxis)){
            buttons[selectedButton].ExecuteButton();
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
                    foreach(MenuButton button in buttons) {
                        if(button && button.GetType() == typeof(LifterButton) && button != buttons[selectedButton]) {
                            LifterButton liftButton = (LifterButton)button;
                            liftButton.tabOpened = false;
                        }
                    }
                    buttons[selectedButton].ExecuteButton();
                }
            }
        }
        ChangeSelector();
    }

    private void ChangeSelector() {
        selector.position = new Vector3(selector.position.x, buttons[selectedButton].transform.position.y + 0.1f, selector.position.z);
    }
}
