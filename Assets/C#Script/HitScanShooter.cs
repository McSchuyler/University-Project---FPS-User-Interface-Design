using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitScanShooter : MonoBehaviour {

    public GameObject weapon;
    public GameObject virtualPointer;

    public GameObject detector;
    public GameObject hitUI;

    public GameObject[] bulletUIs;
    public float rechargeTime = 1.0f;

    private int bulletCount = 5;
    private float counter = 0.0f;

    private Camera camera;

    private Image hitUIImage;

    private GameObject generalController;
    private GameState gameState;
    private Vector3 crosshairPoint;

	void Start () 
    {
        camera = GetComponent<Camera>();

        generalController = GameObject.FindGameObjectWithTag("GeneralController");
        gameState = generalController.GetComponent<GameState>();
      
        crosshairPoint = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, 0);

        hitUIImage = hitUI.GetComponent<Image>();

        for (int i = 0; i < bulletUIs.Length; i++)
        {
            bulletUIs[i].GetComponent<Renderer>().material.color = Color.red;
        }

        bulletCount = 5;
	}
	
	void Update () 
    {
        if (gameState.isPause != true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Ray ray = camera.ScreenPointToRay(crosshairPoint);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    detector.SetActive(true);
                }
                else
                {
                    detector.SetActive(false);
                }

                weapon.transform.LookAt(hit.point);
            }
            else
            {
                weapon.transform.LookAt(virtualPointer.transform.position);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray hitRay = camera.ScreenPointToRay(crosshairPoint);;
                RaycastHit hitHit;

                if(bulletCount > 0)
                {
                    AudioManager.instance.PlaySound("Shoot", transform.position);
                    bulletUIs[bulletCount-1].GetComponent<Renderer>().material.color = Color.white;
                    bulletCount--;
                    if(Physics.Raycast(hitRay, out hitHit))
                        {
                          if (hitHit.transform.gameObject.tag == "Enemy")
                            {
                                StartCoroutine(HitUI());
                            }
                        }
                }

                if (bulletCount <= 0)
                {
                    AudioManager.instance.PlaySound("DryFire", transform.position);
                }
            }

            counter += Time.deltaTime;
            if (counter >= rechargeTime && bulletCount < 5)
            {
                bulletCount++;
                bulletUIs[bulletCount-1].GetComponent<Renderer>().material.color = Color.red;
                counter = 0.0f;
            }

        }
	}

    IEnumerator HitUI()
    {
        Color32 transparentColor = new Color32(225, 225, 225, 0);
        Color32 originalColor = new Color32(225, 225, 225, 225);

        float percent = 0;
        float time = 0.2f;
        float hitCounter = 0;

        while (percent <= 1)
        {
            hitUI.SetActive(true);
            hitCounter += Time.deltaTime;
            percent = hitCounter / time;
            hitUIImage.color = Color.Lerp(originalColor, transparentColor, percent);
            if (percent >= 1)
            {
                hitUI.SetActive(false);
            }

            yield return null;
        }
    }
}
