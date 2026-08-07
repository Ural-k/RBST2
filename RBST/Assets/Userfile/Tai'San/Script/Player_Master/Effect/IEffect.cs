using UnityEngine;

public interface IEffect
{
    /*
     *  バフ・デバフ
     */
    void OnApply(Effect instance);
    void OnTick(Effect instance);
    void OnRemove(Effect instance);
}
