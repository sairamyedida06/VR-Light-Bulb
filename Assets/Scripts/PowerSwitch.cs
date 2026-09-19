using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    // public get so other scripts can read power state, private set so only the switch can change it
    public bool IsPowerOn { get; private set; }

    // Wired to the XR Simple Interactable's Select Entered event
    public void Toggle()
    {
        IsPowerOn = !IsPowerOn;
        Debug.Log("Power is now: " + (IsPowerOn ? "ON" : "OFF"));
    }
}