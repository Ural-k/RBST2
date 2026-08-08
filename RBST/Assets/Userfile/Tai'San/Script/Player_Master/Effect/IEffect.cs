
public interface IEffect
{
    void OnApply(Effect instance);
    void OnTick(Effect instance);
    void OnRemove(Effect instance);
}
