using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public float defaultFadeTime = 2.0f;
    public Color fadeColor = new Color(0.01f, 0.01f, 0.01f, 1.0f);
    public Shader fadeShader = null;

    private Material fadeMaterial = null;
    private bool isFading = false;

    void Awake()
    {
        fadeMaterial = (fadeShader != null) ? new Material(fadeShader) : new Material(Shader.Find("Transparent/Diffuse"));
    }

    void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadSceenWithFade(string sceneName, float _fadeTime)
    {
        StartCoroutine(FadeOut(sceneName, _fadeTime));
    }

    public void LoadSceenWithFade(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    void OnDestroy()
    {
        if (fadeMaterial != null)
        {
            Destroy(fadeMaterial);
        }
    }

    IEnumerator FadeIn() {
        yield return FadeIn(defaultFadeTime);
    }

    IEnumerator FadeIn (float _fadeTime)
    {
        float elapsedTime = 0.0f;
        Color color = fadeMaterial.color = fadeColor;
        isFading = true;
        while (elapsedTime < _fadeTime)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / _fadeTime);
            fadeMaterial.color = color;
        }
        isFading = false;
    }

    IEnumerator FadeOut (string scene) {
        yield return FadeOut(scene, defaultFadeTime);
    }

    IEnumerator FadeOut(string scene, float _fadeTime)
    {
        float elapsedTime = 0.0f;
        fadeColor.a = 0f;
        Color color = fadeMaterial.color = fadeColor;
        isFading = true;
        while (elapsedTime < _fadeTime)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
            color.a = 0.0f + Mathf.Clamp01(elapsedTime / _fadeTime);
            fadeMaterial.color = color;
        }
        Debug.Log("Load Scene: " + scene);
        SceneManager.LoadScene(scene);
        isFading = false;
    }

    void OnPostRender()
    {
        if (isFading)
        {
            fadeMaterial.SetPass(0);
            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Color(fadeMaterial.color);
            GL.Begin(GL.QUADS);
            GL.Vertex3(0f, 0f, -12f);
            GL.Vertex3(0f, 1f, -12f);
            GL.Vertex3(1f, 1f, -12f);
            GL.Vertex3(1f, 0f, -12f);
            GL.End();
            GL.PopMatrix();
        }
    }
}
