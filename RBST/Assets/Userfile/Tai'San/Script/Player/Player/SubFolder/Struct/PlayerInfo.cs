using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public struct PlayerInfo
{
    [SerializeField] public Parameter parameter_;

    [HideInInspector] public float gcd_;
    [HideInInspector] public InputSkillInfo skill1_;
    [HideInInspector] public InputSkillInfo skill2_;
    [HideInInspector] public InputSkillInfo skill3_;
    [HideInInspector] public float activeCombo_;
    [HideInInspector] public JobData skillData_;
    [HideInInspector] public InputAction inputAxis_;
    [HideInInspector] public Vector2 lastFace_;
    [HideInInspector] public int filip_;
    [HideInInspector] public float downTime_;
    [HideInInspector] public int lastInput_;

    /// <summary>
    /// プレイヤーをposの方向へ向かせる
    /// </summary>
    /// <param name="pos">向かせる方向</param>
    /// <param name="me">自身のTransform</param>
    /// <param name="horizontal"></param>
    /// <returns>向かせた方向</returns>
    public Vector2 LookAt(Vector3 pos, Transform me, bool horizontal = false)
    {
        if (me.position != pos)
        {
            lastFace_ = Vector2.Normalize(pos - me.position);
            if (horizontal) lastFace_ *= Vector2.right;
            filip_ = (int)Mathf.Sign(lastFace_.x);
        }
        return lastFace_;
    }
}