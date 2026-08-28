using UnityEngine;

public class AOEWaveEffect : MonoBehaviour
{
    public float duration = 0.6f;
    public float maxRadius = 1f;
    public AnimationCurve alphaCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    Renderer rend;
    MaterialPropertyBlock mpb;
    float timer;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        mpb = new MaterialPropertyBlock();
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        rend.GetPropertyBlock(mpb);
        mpb.SetFloat("_Radius", Mathf.Lerp(0, maxRadius, t));
        mpb.SetFloat("_Alpha", alphaCurve.Evaluate(t));
        rend.SetPropertyBlock(mpb);
    }
}