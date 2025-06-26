using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    private NetworkPlayer trackedPlayer;

    void Update()
    {
        // Falls noch kein Player getrackt wird, einen suchen
        if (trackedPlayer == null)
        {
            foreach (var player in FindObjectsOfType<NetworkPlayer>())
            {
                if (!player.IsLocalPlayer) // nur Remote-Spieler (nicht du selbst)
                {
                    trackedPlayer = player;
                    Debug.Log("Tracking remote player: " + player.name);
                    break;
                }
            }
        }

        // Wenn ein Player gefunden wurde, dessen Headset verfolgen
        if (trackedPlayer != null && trackedPlayer.head != null)
        {
            transform.position = trackedPlayer.head.position;
            transform.rotation = trackedPlayer.head.rotation;
        }
    }
}
