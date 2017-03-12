using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject mainOption;
    public GameObject graphicOption;
    public GameObject audioOption;
    public Slider[] volumeSliders;
    public GameObject logo;

    public Toggle[] resolutionToggles;
    public Toggle fullscreenToggle;
    public int[] screenWidths;

    private GameObject graphicButton;
    private GameObject audioButton;

    private int activeScreenResIndex;

    private Color activeColor;
    private Color disactiveColor;

    void Start()
    {
        mainMenu.SetActive(true);

        activeColor = new Color32(225,225,225,225);
        disactiveColor = new Color32 (125, 125, 125, 67);

        graphicButton = mainOption.transform.FindChild("Graphic Button").gameObject;
        audioButton = mainOption.transform.FindChild("Audio Button").gameObject;

        activeScreenResIndex = PlayerPrefs.GetInt("screen res index"); 
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false; 
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }

        fullscreenToggle.isOn = isFullscreen;

        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;
    }

    public void OptionActive()
    {
        mainMenu.SetActive(false);
        mainOption.SetActive(true);

        logo.GetComponent<LogoAnimator>().glowingBool = false;
    }

    public void GraphicMenuActive()
    {
        graphicOption.SetActive(true);
        audioOption.SetActive(false);
        graphicButton.GetComponent<Image>().color = activeColor;
        audioButton.GetComponent<Image>().color = disactiveColor;
    }

    public void AudioMenuActive()
    {
        graphicOption.SetActive(false);
        audioOption.SetActive(true);
        graphicButton.GetComponent<Image>().color = disactiveColor;
        audioButton.GetComponent<Image>().color = activeColor;
    }

    public void ReturnMenuActive()
    {
        mainMenu.SetActive(true);
        mainOption.SetActive(false);
        graphicOption.SetActive(false);
        audioOption.SetActive(false);
        graphicButton.GetComponent<Image>().color = activeColor;
        audioButton.GetComponent<Image>().color = activeColor;
    }

    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false); 
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }

        PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value) //function to set master volume
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master); //run function in audio manager script to set respective volume
    }

    public void SetMusicVolume(float value) //function to set BGM volume
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music); //run function in audio manager script to set respective volume
    }

    public void SetSfxVolume(float value) //function to set sfx volume
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx); //run function in audio manager script to set respective volume
    }
}
