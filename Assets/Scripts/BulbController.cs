using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class BulbController : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socket;   // Bulb Socket
    [SerializeField] private PowerSwitch powerSwitch;     // the switch
    [SerializeField] private SliderControl slider;        // the brightness slider
    [SerializeField] private BulbLight bulbLight;         // the light executor

    private bool bulbSeated;

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnBulbSeated);
        socket.selectExited.AddListener(OnBulbRemoved);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnBulbSeated);
        socket.selectExited.RemoveListener(OnBulbRemoved);
    }

    private void OnBulbSeated(SelectEnterEventArgs args) => bulbSeated = true;
    private void OnBulbRemoved(SelectExitEventArgs args) => bulbSeated = false;

    private void Update()
    {

        // Light is on only when the bulb is seated AND power is on
        if (bulbSeated && powerSwitch.IsPowerOn)
        {
            bulbLight.SetBrightness(slider.Value);
       
        }
        else
        {
            bulbLight.TurnOff();
        }
            
    }
}