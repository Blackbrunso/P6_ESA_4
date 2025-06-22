using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public GameObject XRSetup;     // z.B. XR-Kamera
    public GameObject altCamera;
    void Start()
    {
#if UNITY_ANDROID
    if (XRSetup != null) XRSetup.SetActive(true);
    if (altCamera != null) altCamera.SetActive(false);
#else
        if (XRSetup != null) XRSetup.SetActive(false);
        if (altCamera != null) altCamera.SetActive(true);
#endif
    }

}
