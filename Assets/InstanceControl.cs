using UnityEngine;

public class InstanceControl : MonoBehaviour
{
    public GameObject XRSetup;     // z.B. XR-Kamera
    public GameObject altCamera;   // z.B. Zuschauerkamera

    public void SetCameraMode(bool useXR)
    {
        XRSetup.SetActive(useXR);
        altCamera.SetActive(!useXR);
    }
}
