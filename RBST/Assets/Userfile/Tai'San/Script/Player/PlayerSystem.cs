using UnityEngine;

public class PlayerSystem : PlayerAttack
{
    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    /// <param name="input_move">入力方向・強さ</param>
    /// <param name="speed">移動速度</param>
    /// <param name="character_controller">CharacterControllerコンポーネント</param>
    public void PlayerMove(Vector2 input,float speed, CharacterController character_controller)
    {
        Vector2 move_value = input;
        move_value *= speed * Time.deltaTime;
        character_controller.Move(move_value);
    }

    public void Primary()
    {
        //CircleAttack();
    }

    public void Secondary()
    {

    }

    public void Special()
    {

    }
}
