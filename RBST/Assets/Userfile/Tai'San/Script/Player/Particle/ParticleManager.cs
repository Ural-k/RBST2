using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

/// <summary>
/// PlayerSkillによるParticleの制御
/// </summary>
public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleEntry[] particleEntries;
    private Dictionary<SkillShape, UnityEngine.Pool.ObjectPool<GameObject>> pool_;
    private static ParticleManager instance_;

    public static ParticleManager Instance { get { return instance_; } }

    [System.Serializable]
    public struct ParticleEntry
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
        foreach (var entry in particleEntries)
        {
            var prefab = entry.prefab_;
            pool_[entry.phape_] = new UnityEngine.Pool.ObjectPool<GameObject>(
                createFunc: () => Instantiate(prefab),
                actionOnGet: ps => ps.SetActive(true),
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
        var prefab = Resources.Load<GameObject>("Particle/ParticleManager");
        instance_ = Instantiate(prefab).GetComponent<ParticleManager>();
        DontDestroyOnLoad(instance_.gameObject);
    }

    /// <summary>
    /// パーティクル呼び出し
    /// </summary>
    /// <param name="targetPos">呼び出し位置</param>
    public GameObject SpawnParticle(SkillData data, Vector2 targetPos, Vector2 myPos, int filip)
    {
        var result = pool_[data.shape_].Get();
        if (data.shape_ == SkillShape.Square)
        {
            result.transform.position = myPos;
            result.transform.localScale = data.scale_ * new Vector2(filip, 1);
        }
        else
        {
            result.transform.position = targetPos;
            result.transform.localScale = data.scale_;
        }
        var particleSystem = result.GetComponent<ParticleSystem>();
        Color color = particleSystem.startColor;
        color.g = 0.5f; //仮
        particleSystem.startColor = color;
        result.transform.SetParent(transform);
        return result;
    }
    public void Release(SkillShape type, GameObject ps) => pool_[type].Release(ps);
}
