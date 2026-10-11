using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public bool enableFreeMove = true;
     public bool enableOrbitCam = false;

// For free movement
    private InspectorController controls;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float moveUpInput;
    private float moveDownInput;
    public float moveSpeed = 5f;
    public float sensitivity = 2f;
    private float rotationX = 0f;
    private float rotationY = 0f;
    public Transform playerBody; // assign the parent object for horizontal rotation (e.g., player)

// For orbit camera
    public bool startOrbitFromCurrentLocation = true;

    public Transform target;        // The object to orbit around
    public float distance = 10f;    // Orbit radius
    public float elevationAngle = 30f; //Tilt of the orbit plane (degrees)
    public float orbitSpeed = 30f;  // Degrees per second

    private float currentAngle = 180f;    private float _distance;

    private void Awake()
    {
        controls = new InspectorController();
    }


    void Start()
    {
        if (startOrbitFromCurrentLocation)
        {
            _distance = Vector3.Distance(this.transform.position, target.transform.position);
             Debug.Log("Distance: " + _distance);
        }
        else
        {
            _distance = distance;
        }
    }

    private void OnEnable()
    {
        controls.freeMove.Enable();

        controls.freeMove.MoveXZ.performed += OnMove;
        controls.freeMove.MoveXZ.canceled += OnMove;

        controls.freeMove.MoveUp.performed += ctx => moveUpInput = ctx.ReadValue<float>();
        controls.freeMove.MoveUp.canceled += ctx => moveUpInput = 0f;

        controls.freeMove.MoveDown.performed += ctx => moveDownInput = ctx.ReadValue<float>();
        controls.freeMove.MoveDown.canceled += ctx => moveDownInput = 0f;

        controls.freeMove.PitchYaw.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.freeMove.PitchYaw.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnDisable()
    {
        controls.freeMove.PitchYaw.performed -= OnMove;
        controls.freeMove.PitchYaw.canceled -= OnMove;
        controls.freeMove.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {

        if (target == null) return;
        if (!enableOrbitCam && !enableFreeMove) return;

        if (enableFreeMove)
        {
                // Translate
                Vector3 move;
                if (moveUpInput > 0)
                {
                    move = new Vector3(moveInput.x, moveUpInput, moveInput.y);
                }
                else if (moveDownInput > 0)
                {
                    move = new Vector3(moveInput.x, -moveDownInput, moveInput.y);
                }
                else
                {
                    move = new Vector3(moveInput.x, 0.0f, moveInput.y);
                }

                //transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
                transform.Translate(move * moveSpeed * Time.deltaTime);

                // Rotate
                float mouseX = lookInput.x * sensitivity;
                float mouseY = lookInput.y * sensitivity;

                rotationX -= mouseX;
                //rotationX = Mathf.Clamp(rotationX, -90f, 90f);
                rotationY -= mouseY;
                rotationY = Mathf.Clamp(rotationY, -90f, 90f);

                transform.localRotation = Quaternion.Euler(rotationY, -rotationX, 0.0f);
                if (playerBody != null)
                    playerBody.Rotate(Vector3.up * mouseX);
        }
            
        if (enableOrbitCam)
        {
                if (target == null) return;
                if (!enableOrbitCam) return;

                // Advance orbit angle
                currentAngle += orbitSpeed * Time.deltaTime;
                if (currentAngle > 360f) currentAngle -= 360f;

                // Compute horizontal circular orbit position in XZ plane
                float radians = currentAngle * Mathf.Deg2Rad;
                Vector3 localPosition = new Vector3(
                    Mathf.Cos(radians) * distance,
                    0,
                    Mathf.Sin(radians) * distance
                );

                // Rotate the orbit plane around the X-axis to apply elevation tilt
                Quaternion tilt = Quaternion.Euler(elevationAngle, 0, 0);
                Vector3 tiltedPosition = tilt * localPosition;

                // Set position relative to target and look at it
                transform.position = target.position + tiltedPosition;
                transform.LookAt(target);
        }
    }
}