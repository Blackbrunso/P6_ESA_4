using UnityEngine;

public class VRRigrefferences : MonoBehaviour
{
    public static VRRigrefferences Singelton;


    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private void Awake()
    {
        Singelton = this;
    }
}
