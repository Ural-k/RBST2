using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TestPlayerBase player_;
    private Dictionary<Sprite, Sprite> buff_;
    private Dictionary<Sprite, Sprite> debuff_;

    [SerializeField] private GameObject prefab;
    private IObjectPool<GameObject> _pool;

    private void Awake()
    {
        //_pool = new ObjectPool<GameObject>(
        //    createFunc: () => Instantiate(prefab),
        //    actionOnGet: obj => obj.SetActive(true),
        //    actionOnRelease: obj => obj.SetActive(false)
        //);
    }

    private void Start()
    {
        player_.Effect.OnBuffApplied += AddEffect;
        player_.Effect.OnBuffRemoved += RemoveEffect;
    }

    /// <summary>
    /// バフ・デバフの整頓
    /// </summary>
    private void Align()
    {

    }

    private void AddEffect(EffectController effect)
    {
        switch (effect.data_.type_)
        {
            case EffectType.Buff: buff_.Add(effect.data_.icon_, effect.data_.arrow_); break;
            case EffectType.Debuff: debuff_.Add(effect.data_.icon_, effect.data_.arrow_); break;
        }
    }

    private void RemoveEffect(EffectController effect)
    {

    }
}
