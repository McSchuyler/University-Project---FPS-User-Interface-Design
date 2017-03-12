using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _charController;
    private GameObject generalController;
    private GameState gameState;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        generalController = GameObject.FindGameObjectWithTag("GeneralController");
        gameState = generalController.GetComponent<GameState>();

    }
    void Update()
    {
        if (gameState.isPause != true)
        {
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charController.Move(movement);
        }

    }
}