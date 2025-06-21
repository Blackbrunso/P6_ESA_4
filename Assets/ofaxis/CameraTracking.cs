using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    
    void Update()
    {
        if (NetworkPlayer.MainHeadset != null)
        {
            //transform.position = test.position; 
            transform.position = NetworkPlayer.MainHeadset.position;
            //transform.rotation = NetworkPlayer.MainHeadset.rotation; 
        }
    }
}
