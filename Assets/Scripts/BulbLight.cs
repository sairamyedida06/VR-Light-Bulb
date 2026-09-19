using UnityEngine;

/// <summary>
/// Controls the bulb's Light. The BulbController decides state; this script just executes it.
/// </summary>
public class BulbLight : MonoBehaviour
{
    [SerializeField] private Light bulbLight;      // the Point Light in the bulb
    [SerializeField] private float maxIntensity = 5f;

    // Set brightness from a 0-1 value
    public void SetBrightness(float value01)
    {
        bulbLight.intensity = value01 * maxIntensity;
    }

    public void TurnOff()
    {
        bulbLight.intensity = 0f;
    }
}