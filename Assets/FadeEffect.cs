using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public float fadeDuration = 1.0f; // Duration of the fade effect
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }


    private void Start()
    {
        StartCoroutine(FadeToTransparent());
    }

    private IEnumerator FadeToBlack()
    {
        Color startColor = rend.material.color;

        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        float timer = 0.0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(startColor.a, targetColor.a, timer / fadeDuration);

            rend.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }

        rend.material.color = targetColor;
    }

    private IEnumerator FadeToTransparent()
    {
        Color startColor = rend.material.color;

        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

        float timer = 0.0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(startColor.a, targetColor.a, timer / fadeDuration);

            rend.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }

        rend.material.color = targetColor;
    }
}
