using UnityEngine;

public class SliderControl : MonoBehaviour
{
    [SerializeField] private float startZ = 0f;      // dim end
    [SerializeField] private float endZ = -0.727f;   // bright end

    public float Value { get; private set; }
    public bool HasBeenUsed { get; private set; }

    private void Update()
    {
        // InverseLerp maps the knobs current Z between the two ends onto a 0-1 range
        Value = Mathf.InverseLerp(startZ, endZ, transform.localPosition.z);

        if (Value > 0.01f)
        {
            HasBeenUsed = true;
        }
    }
}