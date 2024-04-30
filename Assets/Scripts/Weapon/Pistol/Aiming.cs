using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public AimState aim;
    [SerializeField] float minXRotation = -1f; // Minimum X rotation angle
    [SerializeField] float maxXRotation = 1f; // Maximum X rotation angle
    [SerializeField] float minYRotation = -1f; // Minimum Y rotation angle
    [SerializeField] float maxYRotation = 1f; // Maximum Y rotation angle
    [SerializeField] float rotationSpeed;

    void Start()
    {
        aim = GameObject.FindGameObjectWithTag("Player").GetComponent<AimState>();
    }

    void Update()
    {
        transform.LookAt(aim.targetPosition);

        // Get the current rotation Euler angles
        Vector3 currentRotation = transform.localRotation.eulerAngles;

        // Clamp the Y rotation within the specified range
        currentRotation.x = Mathf.Clamp(currentRotation.x, minXRotation, maxXRotation);
        currentRotation.y = Mathf.Clamp(currentRotation.y, minYRotation, maxYRotation);

        // Apply the clamped rotation
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
}
