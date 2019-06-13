using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public SoundValue bgMusic;
    public SettingsMenu settingsMenu;
    private void Start()
    {
        settingsMenu.ShowReturnToMenu(false);
        Director.GetManager<SoundManager>().PlaySound(bgMusic);
    }

    public void Settings()
    {
        settingsMenu.ToggleMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
