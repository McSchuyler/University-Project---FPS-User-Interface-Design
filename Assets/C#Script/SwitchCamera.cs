using UnityEngine;
using System.Collections;

public class SwitchCamera : MonoBehaviour {

    public Camera[] cameras;
    public GameObject[] menuObject;

    private int currentCameraIndex;
    private bool starting = false;

    private Vector3 originalCameraPos;
    private Vector3 playerCameraPos;

    private GameObject generalController;
    private GameState gameState;

    void Start () {
        currentCameraIndex = 0;

        cameras[1].gameObject.SetActive(false);

        originalCameraPos = cameras[0].gameObject.transform.position;
        playerCameraPos = cameras[1].gameObject.transform.position;

        generalController = GameObject.FindGameObjectWithTag("GeneralController");
        gameState = generalController.GetComponent<GameState>();
    }
        

    void Update()
    {
        if (currentCameraIndex != 0)
        {
            StopCoroutine(CameraTranstion());
            if (starting == false)
            {
                gameState.isPause = false;
                gameState.inGame = true;
                starting = true;
            }
        } 
    }

    public void FPScamera()
    {
        for (int i = 0; i < menuObject.Length; i++)
        {
            menuObject[i].SetActive(false);
        }
        StartCoroutine(CameraTranstion());
    }

    IEnumerator CameraTranstion()
    {
        float percent = 0;
        float travelTime = 2.0f;
        float counter = 0;

        while (currentCameraIndex == 0)
        {
            counter += Time.deltaTime;
            percent = counter / travelTime;
            cameras[0].transform.position = Vector3.Lerp(originalCameraPos,playerCameraPos,percent);

            if (percent >= 1)
            {
                currentCameraIndex = 1;
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);
            }
            yield return null;
        }
    }
}
