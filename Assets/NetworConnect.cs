using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NetworkConnect : MonoBehaviour
{
    

    [Header("Visuelles Feedback")]
    public Renderer floorRenderer;
    public Material connectedMaterial;
    public Material defaultMaterial;

    bool isConeected= false;

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

    private void Update()
    {
        
            
        if (NetworkManager.Singleton.IsConnectedClient)
        {
            SetFloorConnectedVisual(true);

        }
        else
        {
            SetFloorConnectedVisual(false);
        }
        
        
       
    }

  

    private void SetFloorConnectedVisual(bool connected)
    {
        if (floorRenderer != null && connectedMaterial != null && defaultMaterial != null)
        {
            floorRenderer.material = connected ? connectedMaterial : defaultMaterial;
            //Debug.Log(" Bodenmaterial aktualisiert: " + (connected ? "Verbunden" : "Nicht verbunden"));
        }
        else
        {
            floorRenderer.material = defaultMaterial;
            //Debug.LogWarning(" Floor Renderer oder Materialien fehlen!");
        }
    }


    private void OnStickClick(InputAction.CallbackContext context)
    {
        Debug.Log("Stick gedrückt – Versuche als Client zu joinen...");
        Join();
    }

    public void Create()
    {
        

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
            Debug.Log("Client wird gestartet...");
            NetworkManager.Singleton.StartClient();

        }
        else
        {
            Debug.Log("Client oder Host läuft bereits.");
        }
    }

}
