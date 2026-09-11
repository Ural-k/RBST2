using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// その他機能
/// </summary>
public class PlayerBase : PlayerSkill, IDamageable, IToEnemyDamageAble//Avatar
{
    private Vector3 moveInput_;
    void Awake()
    {
        ParticleManager.InstanceLoad();
    }

    //private void Start()
    //{

    //    if (IsOwner)
    //    {
    //        PlayerInput playerInput = GetComponent<PlayerInput>();
    //        playerInput.ActivateInput();
    //        playerInput.SwitchCurrentActionMap("Player");
    //    }
        
    //}

    /// <summary>
    /// ネットワークオブジェクトが生成されたときうごくメソッド
    /// </summary>
    public override void OnNetworkSpawn()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();

        PlayerManager.AddPlayer(GetComponent<Player>());

        Debug.Log(
        $"PlayerManager Count:{PlayerManager.GetAllPlayerListCount()} IsOwner:{IsOwner} OwnerClientId:{OwnerClientId}"
        );

        info_.effect_.GetAllBuff();
        info_.Initialize(transform, playerInput);

        SetupInput(playerInput);

        StartCoroutine(CoolTimeCoroutine());
    }

    /// <summary>
    /// InputSystemを対応させるメソッド
    /// </summary>
    private void SetupInput(PlayerInput playerInput)
    {
        if (!IsOwner)
        {
            playerInput.DeactivateInput();
            return;
        }

        playerInput.enabled = true;

        AssignControlScheme(playerInput);

        playerInput.ActivateInput();
        playerInput.SwitchCurrentActionMap("Player");

        info_.inputAxis_ = playerInput.actions.FindAction("Move");
        info_.inputAxis_?.Enable();
    }

    public override void OnNetworkDespawn()
    {
        PlayerManager.DeletePlayer(GetComponent<Player>());
    }

    /// <summary>
    /// 入力機器がキーマウかPADかを判別（絶対別に機能あるべ)
    /// </summary>
    private void AssignControlScheme(PlayerInput playerInput)
    {
        //ゲームパッドがあったらPlayerInputをゲームパッド用に
        if (Gamepad.current != null)
        {
            playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
            return;
        }

        //無かったらキーマウ用に
        playerInput.SwitchCurrentControlScheme(
            "Keyboard&Mouse",
            Keyboard.current,
            Mouse.current
        );
    }


    /// <summary>
    /// 移動
    /// </summary>
    protected virtual void PlayerMove()
    {

        if(!IsOwner) {return;}
        Vector2 move_value = info_.inputAxis_.ReadValue<Vector2>();
        info_.LookAt(move_value + (Vector2)transform.position, transform);
        move_value *= info_.parameter_.speed_ * Time.deltaTime;
        Vector3 result = new Vector2(
                Mathf.Clamp(transform.position.x + move_value.x, -MOVE_SCREEN_X, MOVE_SCREEN_X),
                Mathf.Clamp(transform.position.y + move_value.y, -MOVE_SCREEN_Y, MOVE_SCREEN_Y)
            );

        transform.position = result;

    }

 

    public void TakeDamage(int damage)
    {
        info_.parameter_.HP -= damage;
        ShowFloatingText(damage, FloatingTextType.PlayerDamage);
        if (info_.parameter_.HP == 0)
        {
            info_.downTime_ = DOWN_TIME;
            Death();
        }
    }

    public void DamageAble(int damage)
    {
        info_.parameter_.HP -= damage;
        if (info_.parameter_.HP == 0)
        {
            info_.downTime_ = DOWN_TIME;
            Death();
        }
    }

    private void ShowFloatingText(int value, FloatingTextType type)
    {
        if (DamageTextManager.Instance == null) return;

        DamageTextManager.Instance.Show(transform.position, value, type);
    }

    void Death()
    {

    }
}