using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    // public get so other scripts can read power state, private set so only the switch can change it
    public bool IsPowerOn { get; private set; }

    [SerializeField] private Animator animator;

    // Wired to the XR Simple Interactable's Select Entered event
    public void Toggle()
    {
        IsPowerOn = !IsPowerOn;
        animator.SetBool("isOn", IsPowerOn);
        Debug.Log("Power is now: " + (IsPowerOn ? "ON" : "OFF"));
    }
}