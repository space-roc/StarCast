using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    // Rotation speed in degrees per second
    public Vector3 rotationSpeed = new Vector3(0, 30, 0);

    void Update()
    {
        // Rotate the object each frame
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}