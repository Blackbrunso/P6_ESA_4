using UnityEngine;

public class ActivateDisplays : MonoBehaviour
{
    void Start()
    {
        Debug.Log("detected screens number: " + Display.displays.Length);

        
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
            Debug.Log("activated Display " + (i + 1));
        }
    }
}

