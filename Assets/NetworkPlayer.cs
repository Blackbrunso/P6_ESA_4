using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public GameObject network;

    public Renderer[] disable;

    public static Transform MainHeadset;

    private bool allowTracking = false;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            
            allowTracking = !IsHost;

            foreach (var item in disable)
                item.enabled = false;

            GameObject networkManager = GameObject.FindGameObjectWithTag("Network");
            if (networkManager != null)
            {
                InstanceControl control = networkManager.GetComponent<InstanceControl>();
                if (control != null)
                {
                    control.SetCameraMode(allowTracking);
                }
            }

            if (allowTracking)
            {
                MainHeadset = head;
                Debug.Log("MainHeadset assigned: " + head.name);
            }

        }
    }

    void Update()
    {
        if (!IsOwner || !allowTracking) return;

        root.position = VRRigrefferences.Singelton.root.position;
        root.rotation = VRRigrefferences.Singelton.root.rotation;

        head.position = VRRigrefferences.Singelton.head.position;
        head.rotation = VRRigrefferences.Singelton.head.rotation;

        leftHand.position = VRRigrefferences.Singelton.leftHand.position;
        leftHand.rotation = VRRigrefferences.Singelton.leftHand.rotation;

        rightHand.position = VRRigrefferences.Singelton.rightHand.position;
        rightHand.rotation = VRRigrefferences.Singelton.rightHand.rotation;
    }
}
