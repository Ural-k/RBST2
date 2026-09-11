using UnityEngine;

public class AOEFlatFade : MonoBehaviour
{
    //public float duration = 0.25f; // 0.2~0.3秒
    //public AnimationCurve alphaCurve = AnimationCurve.Linear(0, 1, 1, 0);

    //Renderer rend;
    //MaterialPropertyBlock mpb;
    //float timer;

    //void OnEnable()
    //{
    //    rend = GetComponent<MeshRenderer>();
    //    mpb = new MaterialPropertyBlock();
    //    timer = 0f;
    //}

    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    float t = Mathf.Clamp01(timer / duration);

    //    rend.GetPropertyBlock(mpb);
    //    mpb.SetFloat("_Alpha", alphaCurve.Evaluate(t));
    //    rend.SetPropertyBlock(mpb);

    //    if (t >= 1f)
    //    {
    //        // ObjectPoolを使っているのでDestroyではなくReleaseする想定
    //        // 呼び出し側でPool管理している場合はここでイベント発行するか、
    //        // AOEManager側でコルーチン/タイマー管理して pool_.Release(aoe) する
    //    }
    //}
}