using UnityEngine;
using UnityEngine.InputSystem;

//機能の使用・独自のクラスがない機能の宣言
public class PlayerBase : PlayerSkill, IDamageable
{
    private void Start()
    {
        PlayerManager.AddPlayer((Player)this);
        inputAxis_ = InputSystem.actions.FindAction("Move");
        StartCoroutine(SkillCoroutine());


        for (int i = 0; i < 4; ++i)
        {
            demoEnemyList_.Add(GameObject.Find($"Enemy{i + 1}").transform);
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    protected virtual void PlayerMove()
    {
        Vector2 move_value = inputAxis_.ReadValue<Vector2>();
        move_value *= parameter_.speed_ * Time.deltaTime;
        transform.position += (Vector3)move_value;
    }

    public void TakeDamage(int damage_)
    {
        Debug.Log("当たっちゃったーワイプワイプ");
    }
}