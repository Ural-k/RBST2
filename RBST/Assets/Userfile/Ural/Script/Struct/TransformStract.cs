using UnityEngine;

[System.Serializable]
public struct EnemyAttackStract 
{
    [Header("ダメージ")]
    public int damage;

    [Header("生成位置")]
    public Vector2 pos;

    [Header("角度")]
    public float angle;

    [Header("サイズ")]
    public Vector2 scale;

    [Header("内側の安置の円")]
    public float innerRadius;

    [Header("使う攻撃の種類")]
    public AOECollect aoeCollect;

    [Header("予兆の時間")]
    public float warningTime;

    [Header("攻撃判定がで出る時間")]
    public float entityTime;

    [Header("次の攻撃を出すまでの時間")]
    public float nextAttackTime;
}
