using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectCollection : MonoBehaviour
{
    public static EffectCollection Instance;

    private List<EffectData> effects_;

    /// <summary>
    /// Œø‰Ê‚ðŽæ“¾
    /// </summary>
    public void Initialize()
    {
        effects_ = Resources.LoadAll<EffectData>("EffectDatas").ToList();
    }

    /// <summary>
    /// ID‚ÅŒø‰Ê‚ðŽæ“¾
    /// </summary>
    /// <param name="effectId"></param>
    public EffectData GetEffect(int effectId)
    {
        return effects_.Where(n => n.id_ == effectId).FirstOrDefault();
    }
}
