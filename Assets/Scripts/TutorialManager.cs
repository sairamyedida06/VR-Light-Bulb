using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TutorialManager : MonoBehaviour
{
    [Header("Interaction sources")]
    [SerializeField] private XRGrabInteractable bulbGrab;   
    [SerializeField] private XRSocketInteractor socket;     
    [SerializeField] private PowerSwitch powerSwitch;       
    [SerializeField] private SliderControl slider;

    [Header("Prompt canvases (near each item)")]
    [SerializeField] private GameObject grabText;
    [SerializeField] private GameObject socketText;
    [SerializeField] private GameObject powerText;
    [SerializeField] private GameObject sliderText;

    private bool bulbGrabbed;
    private bool bulbSeated;
    private int step = 0;   // 0 grab, 1 socket, 2 power, 3 slider, 4 done

    private void OnEnable()
    {
        bulbGrab.selectEntered.AddListener(OnGrabbed);
        socket.selectEntered.AddListener(OnSeated);
    }

    private void OnDisable()
    {
        bulbGrab.selectEntered.RemoveListener(OnGrabbed);
        socket.selectEntered.RemoveListener(OnSeated);
    }

    private void OnGrabbed(SelectEnterEventArgs args) => bulbGrabbed = true;
    private void OnSeated(SelectEnterEventArgs args) => bulbSeated = true;

    private void Start()
    {
        ShowOnly(grabText);   // start at step 0
    }

    private void Update()
    {
        switch (step)
        {
            case 0:
                if (bulbGrabbed) { step = 1; ShowOnly(socketText); }
                break;
            case 1:
                if (bulbSeated) { step = 2; ShowOnly(powerText); }
                break;
            case 2:
                if (powerSwitch.IsPowerOn) { step = 3; ShowOnly(sliderText); }
                break;
            case 3:
                if (slider.HasBeenUsed) { step = 4; ShowOnly(null); }  // hide all, done
                break;
        }
    }

    // Enable one prompt, disable the rest
    private void ShowOnly(GameObject prompt)
    {
        grabText.SetActive(prompt == grabText);
        socketText.SetActive(prompt == socketText);
        powerText.SetActive(prompt == powerText);
        sliderText.SetActive(prompt == sliderText);
    }
}