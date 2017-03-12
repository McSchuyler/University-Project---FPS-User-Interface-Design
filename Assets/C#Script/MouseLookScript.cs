using UnityEngine;
using System.Collections;

public class MouseLookScript : MonoBehaviour {
    
    public enum RotateAxis
    {
        mouseXandY = 0,
        mouseX = 1,
        mouseY = 2
    }

    public RotateAxis axis = RotateAxis.mouseXandY;

    public float sensitivityHor = 9f;
    public float sensitivityVer = 9f;

    public float minVer = -45f;
    public float maxVer = 45f;

    private float camRotationX = 0;

    private GameObject generalController;
    private GameState gameState;


	void Start () 
    {
        generalController = GameObject.FindGameObjectWithTag("GeneralController");
        gameState = generalController.GetComponent<GameState>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameState.isPause != true)
        {
            if (axis == RotateAxis.mouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            }
            else if (axis == RotateAxis.mouseY)
            {
                camRotationX -= Input.GetAxis("Mouse Y") * sensitivityVer;
                camRotationX = Mathf.Clamp(camRotationX, minVer, maxVer);

                float camRotationY = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(camRotationX, camRotationY, 0);
            }
            else
            {
                camRotationX -= Input.GetAxis("Mouse Y") * sensitivityVer;
                camRotationX = Mathf.Clamp(camRotationX, minVer, maxVer);

                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                float camRotationY = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(camRotationX, camRotationY, 0);
            }
        }
    }
}
