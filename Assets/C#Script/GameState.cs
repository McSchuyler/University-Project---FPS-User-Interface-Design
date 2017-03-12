using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public bool isPause = true;
    public bool inGame = false;
    public GameObject mainOption;
    public GameObject graphicOption;
    public GameObject audioOption;
    public GameObject returnButton;
    public GameObject notification;
    public GameObject quitButton;
    public GameObject crosshair;
    public GameObject health;

    void Update()
    {
        if (isPause == false)
        {
            crosshair.SetActive(true);
            health.SetActive(true);
        }

        if (inGame == true)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (isPause == false)
                {
                    mainOption.SetActive(true);
                    quitButton.SetActive(true);
                    notification.SetActive(true);
                    returnButton.SetActive(false);

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    isPause = true;
                }

                else if (isPause == true)
                {
                    mainOption.SetActive(false);
                    audioOption.SetActive(false);
                    quitButton.SetActive(false);
                    graphicOption.SetActive(false);
                    notification.SetActive(false);

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    isPause = false;
                }
            }
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
