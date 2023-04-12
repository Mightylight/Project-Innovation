using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public float fadeDuration = 1.0f; // Duration of the fade effect
    private Renderer rend;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Start()
    {
        FadeToTransparent();
    }

    public void FadeToBlack(bool loadServerScene = false)
    {
        if(currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(FadeToBlackEnum(loadServerScene));
    }

    public void FadeToTransparent()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(FadeToTransparentEnum());
    }

    private IEnumerator FadeToBlackEnum(bool loadServerScene = false)
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
        if (loadServerScene) SceneManager.LoadScene("VRScene");
    }

    private IEnumerator FadeToTransparentEnum()
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
