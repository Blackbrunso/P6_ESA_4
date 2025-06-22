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

            if (VRRigrefferences.Singelton == null) return;

            if (root != null && VRRigrefferences.Singelton.root != null)
            {
                root.position = VRRigrefferences.Singelton.root.position;
                root.rotation = VRRigrefferences.Singelton.root.rotation;
            }

            if (head != null && VRRigrefferences.Singelton.head != null)
            {
                head.position = VRRigrefferences.Singelton.head.position;
                head.rotation = VRRigrefferences.Singelton.head.rotation;
            }

            if (leftHand != null && VRRigrefferences.Singelton.leftHand != null)
            {
                leftHand.position = VRRigrefferences.Singelton.leftHand.position;
                leftHand.rotation = VRRigrefferences.Singelton.leftHand.rotation;
            }

            if (rightHand != null && VRRigrefferences.Singelton.rightHand != null)
            {
                rightHand.position = VRRigrefferences.Singelton.rightHand.position;
                rightHand.rotation = VRRigrefferences.Singelton.rightHand.rotation;
            }
        

    }
}
