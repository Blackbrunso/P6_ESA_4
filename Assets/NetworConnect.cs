using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NetworkConnect : MonoBehaviour
{
    [Header("Verbindungsdaten")]
    public string ipAddress = "127.0.0.1";
    public ushort port = 7777;

    [Header("Steuerung")]
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
    void Awake()
    {
        if (NetworkManager.Singleton != null && NetworkManager.Singleton != GetComponent<NetworkManager>())
        {
            Debug.LogWarning("Ein weiterer NetworkManager existiert bereits.");
            Destroy(gameObject);
            return;
        }
    }


    private void ConfigureTransport()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport != null)
        {
            transport.SetConnectionData(ipAddress, port);
            Debug.Log($"Transport konfiguriert mit IP: {ipAddress}, Port: {port}");
        }
        else
        {
            Debug.LogWarning("Kein UnityTransport gefunden!");
        }
    }

    private void OnStickClick(InputAction.CallbackContext context)
    {
        Debug.Log("Stick gedrückt – Versuche als Client zu joinen...");
        Join();
    }

    public void Create()
    {
        ConfigureTransport();

        if (!NetworkManager.Singleton.IsListening)
        {
            NetworkManager.Singleton.StartHost();
            Debug.Log("Host gestartet.");
            NetworkManager.Singleton.SceneManager.LoadScene("PC", LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("Bereits verbunden.");
        }
    }

    public void Join()
    {
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost)
        {
            ConfigureTransport();
            Debug.Log("Client wird gestartet...");
            NetworkManager.Singleton.StartClient();
        }
        else
        {
            Debug.Log("Client oder Host läuft bereits.");
        }
    }
}
