using UnityEngine;

public class SliderControl : MonoBehaviour
{
    [SerializeField] private Transform track;   // the Slider Base
    [SerializeField] private float startZ = 0f;      // dim end
    [SerializeField] private float endZ = -0.727f;   // bright end

    public float Value { get; private set; }
    public bool HasBeenUsed { get; private set; }

    private void Update()
    {
        // InverseLerp maps the knobs current Z between the two ends onto a 0-1 range
        float localZ = track.InverseTransformPoint(transform.position).z;
        Value = Mathf.InverseLerp(startZ, endZ, localZ);

        if (Value > 0.01f)
        {
            HasBeenUsed = true;
        }
    }
}