using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerSkillによるParticleの制御
/// </summary>
public class ParticleManager : MonoBehaviour
{
    const float MINCOLOR = 0.0f;
    const float MAXCOLOR = 0.5f;
    const int   MINPOWER = 100;
    const int   MAXPOWER = 1000;

    [SerializeField] private SetParticle[] setParticles_;
    private Dictionary<SkillShape, UnityEngine.Pool.ObjectPool<GameObject>> pool_;

    private static ParticleManager                                          instance_;
    public static ParticleManager                                           Instance { get { return instance_; } }

    [System.Serializable]
    public struct SetParticle
    {
        public SkillShape phape_;
        public GameObject prefab_;
    }

    /*
     :  Pool初期化
     */
    void Awake()
    {
        pool_ = new Dictionary<SkillShape, UnityEngine.Pool.ObjectPool<GameObject>>();
        foreach (var entry in setParticles_)
        {
            var prefab = entry.prefab_;
            pool_[entry.phape_]     = new UnityEngine.Pool.ObjectPool<GameObject>(
                createFunc: ()      => Instantiate(prefab),
                actionOnGet: ps     => ps.SetActive(true),
                actionOnRelease: ps => ps.SetActive(false),
                actionOnDestroy: ps => Destroy(ps)
            );
        }
    }

    /// <summary>
    /// マネージャー呼び出し
    /// </summary>
    /// <remarks>呼び出し以降ParticleManagerによってプレイヤーのパーティクルを制御</remarks>
    public static void InstanceLoad()
    {
        if (instance_ != null) return;
        var prefab  = Resources.Load<GameObject>("Particle/ParticleManager");
        instance_   = Instantiate(prefab).GetComponent<ParticleManager>();
        DontDestroyOnLoad(instance_.gameObject);
    }

    /// <summary>
    /// パーティクル呼び出し
    /// </summary>
    /// <param name="targetPos">呼び出し位置</param>
    public GameObject SpawnParticle(SkillData data, Vector2 targetPos, Vector2 myPos, int filip)
    {
        var result = pool_[data.shape_].Get();

        //矩形のみ特殊
        if (data.shape_ == SkillShape.Square)
        {
            result.transform.position   = myPos;
            result.transform.localScale = data.scale_ * new Vector2(filip, 1);
        }
        else
        {
            result.transform.position   = targetPos;
            result.transform.localScale = data.scale_;
        }
        var particleSystem = result.GetComponent<ParticleSystem>();

        //威力MINPOWER~MAXPOWERでMAXCOLOR~MINCOLORの中で数値が変わる
        Color color = particleSystem.startColor;
        color.g = Mathf.Clamp(MAXCOLOR * (MINPOWER - (data.power_ - MINPOWER) / (MAXPOWER - MINPOWER)), MINCOLOR, MAXCOLOR);
        Debug.Log(color.g);
        particleSystem.startColor = color;

        //親をマネージャーに
        result.transform.SetParent(transform);
        return result;
    }

    /// <summary>
    /// パーティクル呼び出し
    /// </summary>
    /// <param name="targetPos">呼び出し位置</param>
    public GameObject SpawnParticleCircle(Vector2 pos, Vector2 scale, int power, int filip)
    {
        var result = pool_[SkillShape.Circle].Get();

        result.transform.position = pos;
        result.transform.localScale = scale;
        var particleSystem = result.GetComponent<ParticleSystem>();

        //威力MINPOWER~MAXPOWERでMAXCOLOR~MINCOLORの中で数値が変わる
        Color color = particleSystem.startColor;
        color.g = Mathf.Clamp(MAXCOLOR * (MINPOWER - (power - MINPOWER) / (MAXPOWER - MINPOWER)), MINCOLOR, MAXCOLOR);
        Debug.Log(color.g);
        particleSystem.startColor = color;

        //親をマネージャーに
        result.transform.SetParent(transform);
        return result;
    }

    public void Release(SkillShape type, GameObject ps) => pool_[type].Release(ps);
}
