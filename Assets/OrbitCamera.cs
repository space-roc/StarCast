using UnityEngine;

public class OrbitCamera : MonoBehaviour
{

    public bool enableOrbitCam = false;

    public bool startOrbitFromCurrentLocation = true;

    public Transform target;        // The object to orbit around
    public float distance = 10f;    // Orbit radius
    public float elevationAngle = 30f; //Tilt of the orbit plane (degrees)
    public float orbitSpeed = 30f;  // Degrees per second

    private float currentAngle = 0f;

    private float _distance;

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

    void Update()
    {
        if (target == null)return;
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

