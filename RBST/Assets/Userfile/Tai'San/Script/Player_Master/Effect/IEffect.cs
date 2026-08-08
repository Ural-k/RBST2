
public interface IEffect
{
    /// <summary>
    /// 付与時の呼び出し
    /// </summary>
    void OnApply(EffectController instance);
    /// <summary>
    /// 毎秒の呼び出し
    /// </summary>
    /// <param name="instance"></param>
    void OnTick(EffectController instance);
    /// <summary>
    /// 解除時の呼び出し
    /// </summary>
    void OnRemove(EffectController instance);
}
