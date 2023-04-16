using UnityEngine;

public class FlameAnimation : MonoBehaviour
{
    public Light flameLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float speed = 1.0f;

    private float targetIntensity;
    private float currentIntensity;

    private void Start()
    {
        // Initialize targetIntensity and currentIntensity to a random value within the specified range
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        currentIntensity = flameLight.intensity;
    }

    private void Update()
    {
        // Smoothly interpolate currentIntensity towards targetIntensity
        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, speed * Time.deltaTime);

        // Update the intensity of the flame light
        flameLight.intensity = currentIntensity;

        // If currentIntensity is close to targetIntensity, generate a new random targetIntensity
        if (Mathf.Abs(currentIntensity - targetIntensity) < 0.1f)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
