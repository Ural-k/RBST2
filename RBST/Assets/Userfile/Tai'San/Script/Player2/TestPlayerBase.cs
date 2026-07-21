using UnityEngine;
using UnityEngine.InputSystem;

public abstract class TestPlayerBase : MonoBehaviour
{
    protected const float MOVE_SCREEN_X = 8.8f;
    protected const float MOVE_SCREEN_Y = 4.8f;

    [SerializeField] protected TestParameter parameter_;
    public int HP { get { return parameter_.hp_; } set { parameter_.hp_ = value; } }
    Vector2 look_;
    float gcd_;

    protected abstract void Skill1();
    protected abstract void Skill2();
    protected abstract void Skill3();

    public void InputSkill1(InputAction.CallbackContext context) { if (context.performed) Skill1(); }
    public void InputSkill2(InputAction.CallbackContext context) { if (context.performed) Skill2(); }
    public void InputSkill3(InputAction.CallbackContext context) { if (context.performed) Skill3(); }

    /// <summary>
    /// ˆÚ“®
    /// </summary>
    /// <param name="context"></param>
    public void PlayerMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        value *= parameter_.spd_ * Time.deltaTime;
        Vector3 resultPos = new Vector2(
                Mathf.Clamp(transform.position.x + value.x, -MOVE_SCREEN_X, MOVE_SCREEN_X),
                Mathf.Clamp(transform.position.y + value.y, -MOVE_SCREEN_Y, MOVE_SCREEN_Y)
            );
        transform.position = resultPos;
    }

    private void Awake()
    {
        PlayerManager.AddPlayer(new Player());
    }
}

[System.Serializable]
public struct TestParameter
{
    public int hp_;
    public int maxHp_;
    public int atk_;
    public int def_;
    public int spd_;
}