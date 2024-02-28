using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Numerics;

public class AimState : MonoBehaviour
{
    public AxisState xAxis, yAxis;
    [SerializeField] Transform camFollow;
    [SerializeField] float mouseSense;
    [SerializeField] float aimSmoothSpeed = 20f;
    [SerializeField] LayerMask aimMask;
    public CinemachineVirtualCamera vCam;
    // public float adsFov = 40f;
    // public float hipFov;
    public float currentFov;
    public float fovSmoothSpeed = 10f;

    void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        // hipFov = vCam.m_Lens.FieldOfView;
    }

    private void Update()
    {
        xAxis.Value += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis.Value -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis.Value = Mathf.Clamp(yAxis.Value, -80, 80);

        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            currentFov = 25f;
        }
        else
        {
            currentFov = 50f;
        }
    }

    void LateUpdate()
    {
        camFollow.localEulerAngles = new UnityEngine.Vector3(yAxis.Value, camFollow.localEulerAngles.y, camFollow.localEulerAngles.z);
        transform.eulerAngles = new UnityEngine.Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}
