using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float zoomSpeed = 12f;
    [SerializeField] private float minDistance = 6f;
    [SerializeField] private float maxDistance = 60f;
    [SerializeField] private float startDistance = 20f;

    private float basePitch;
    private float yaw;
    private float distance;
    private Vector3 pivot;

    private void Start()
    {
        var e = transform.rotation.eulerAngles;
        basePitch = e.x;
        yaw = e.y;
        distance = Mathf.Clamp(startDistance, minDistance, maxDistance);
        pivot = transform.position + transform.forward * distance;
        ApplyTransform();
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        float rotInput = Input.GetAxisRaw("Horizontal");
        yaw += rotInput * rotateSpeed * dt;

        float moveInput = Input.GetAxisRaw("Vertical");
        Vector3 forwardPlane = Vector3.ProjectOnPlane(Quaternion.Euler(0f, yaw, 0f) * Vector3.forward, Vector3.up).normalized;
        pivot += forwardPlane * (moveInput * moveSpeed * dt);

        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.0001f)
        {
            distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);
        }

        ApplyTransform();
    }

    private void ApplyTransform()
    {
        transform.rotation = Quaternion.Euler(basePitch, yaw, 0f);
        transform.position = pivot - transform.forward * distance;
    }
}
