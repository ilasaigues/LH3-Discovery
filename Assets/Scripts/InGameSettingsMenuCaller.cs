using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSettingsMenuCaller : MonoBehaviour
{
    public SettingsMenu settingsMenu;
    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.ShowReturnToMenu(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) settingsMenu.ToggleMenu();
    }
}
