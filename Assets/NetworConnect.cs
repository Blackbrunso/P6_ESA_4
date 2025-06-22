using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NetworkConnect : MonoBehaviour
{
    public InputAction stickClickAction; 

    private void OnEnable()
    {
        stickClickAction.Enable();
        stickClickAction.performed += OnStickClick;
    }

    private void OnDisable()
    {
        stickClickAction.performed -= OnStickClick;
        stickClickAction.Disable();
    }

    private void OnStickClick(InputAction.CallbackContext context)
    {
        Debug.Log("Joining as client...");
        Join();
    }

    public void Create()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("PC", LoadSceneMode.Single);
    }

    public void Join()
    {
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost)
        {
            Debug.Log("Joining as client...");
            NetworkManager.Singleton.StartClient();
        }
        else
        {
            Debug.Log("Already connected.");
        }
    }

}
