using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Net.NetworkInformation;

public class AimState : MonoBehaviour
{
    public AxisState xAxis, yAxis;
    [SerializeField] Transform camFollow;
    [SerializeField] float mouseSense;
    public CinemachineVirtualCamera vCam;
    // public float adsFov = 40f;
    // public float hipFov;
    public float normalFov;
    public float aimFov;
    public float fovSmoothSpeed = 10f;
    public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20f;
    [SerializeField] LayerMask aimMask;

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

        float targetFov = Input.GetMouseButton(1) ? aimFov : normalFov;
        ChangeFov(targetFov);

        Vector2 screenCenter = new(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        camFollow.localEulerAngles = new Vector3(yAxis.Value, camFollow.localEulerAngles.y, camFollow.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }

    public void ChangeFov(float currentFov)
    {
        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);
    }
}
