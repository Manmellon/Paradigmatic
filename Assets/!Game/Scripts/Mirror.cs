using UnityEngine;

public class Mirror : MonoBehaviour
{
    Camera mainCamera;
    Camera mirrorCamera;

    Transform pivot;

    private void Start()
    {
        mainCamera = Camera.main;
        mirrorCamera = GetComponentInChildren<Camera>();

        pivot = transform.GetChild(0);
    }

    private void LateUpdate()
    {
        // Position
        Vector3 planeNormal = pivot.forward;
        Vector3 planePoint = pivot.position;
        Plane mirrorPlane = new Plane(planeNormal, planePoint);
        Vector3 projectedPoint = mirrorPlane.ClosestPointOnPlane(mainCamera.transform.position);

        mirrorCamera.transform.position = 2f * projectedPoint - mainCamera.transform.position;

        // Rotation
        Vector3 reflectedForward = Vector3.Reflect(mainCamera.transform.forward, planeNormal);
        Vector3 reflectedUp = Vector3.Reflect(mainCamera.transform.up, planeNormal);
        mirrorCamera.transform.rotation = Quaternion.LookRotation(reflectedForward, reflectedUp);

        // Camera settings
        mirrorCamera.fieldOfView = mainCamera.fieldOfView;
        mirrorCamera.aspect = mainCamera.aspect;

    }
}
