using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SettingsMenu : SlideInMenu
{
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider fxVolume;

    public GameObject returnToMenuButton;

    private void Start()
    {
        masterVolume.value = PlayerPrefs.GetFloat("Volume_Master", 1);
        musicVolume.value = PlayerPrefs.GetFloat("Volume_Music", 1);
        fxVolume.value = PlayerPrefs.GetFloat("Volume_FX", 1);

    }

    public void ShowReturnToMenu(bool show)
    {
        returnToMenuButton.SetActive(show);
    }


    public void UpdateMasterVolume()
    {
        Director.GetManager<SoundManager>().SetMasterVolumeScalar(masterVolume.value);
    }

    public void UpdateFXVolume()
    {
        Director.GetManager<SoundManager>().SetFXVolumeScalar(fxVolume.value);
    }

    public void UpdateMusicVolume()
    {

        Director.GetManager<SoundManager>().SetMusicVolumeScalar(musicVolume.value);

    }

    public void Return()
    {
        ToggleMenu();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
