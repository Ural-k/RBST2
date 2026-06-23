using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControl : MonoBehaviour, IToEnemyDamageAble
{
    [SerializeField] private EnemyAtackObjectPool pool_;
    [SerializeField] private EnemyAttackStatus[] scriptableObject_;
    [SerializeField] private int maxHp_ = 10000;
    [SerializeField] private int hp_ = 10000;

    private AOECollect colect_;
    private EnemyAttackStract eaStruct;
    private int attackPhase = 0;

    private int waitTime = 5;
    private bool entryFlag_;

    public int HP { get { return hp_; } }
    public int MaxHP { get { return maxHp_; } }

    private void Awake()
    {
        entryFlag_ = false;
        StartCoroutine(GimmickCorutine());
    }

    void Start()
    {
        eaStruct = new EnemyAttackStract();
        eaStruct.pos = transform.position;
        eaStruct.innerRadius = 0f;
        eaStruct.scale = Vector2.one;
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    public void DamageAble(int damage)
    {
        hp_ = Mathf.Max(hp_ - damage, 0);
        ShowFloatingText(damage, FloatingTextType.EnemyDamage);
        if (hp_ == 0)
        {
            Died();
        }
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    public void Died()
    {
        EnemyManager.DeleteEnemy(this);
        Destroy(gameObject);
    }

    /// <summary>
    /// 敵の回復（使うかはわからない）
    /// </summary>
    public void Heal(int heal)
    {
        hp_ = Mathf.Min(hp_ + heal, maxHp_);
        ShowFloatingText(heal, FloatingTextType.Heal);
    }

    /// <summary>
    /// ダメージの呼び出し
    /// </summary>
    private void ShowFloatingText(int value, FloatingTextType type)
    {
        if (DamageTextManager.Instance == null) return;

        DamageTextManager.Instance.Show(transform.position, value, type);
    }

    public IEnumerator GimmickCorutine()
    {
        var wait = new WaitForSeconds(waitTime);
        var attackWait = new WaitForSeconds(3);
        while (true)
        {
            if (!entryFlag_)
            {
            }


            yield return attackWait;

            if (scriptableObject_.Length <= attackPhase) { attackPhase = 0; }
            var data = scriptableObject_[attackPhase];
            for (int attackCount = 0; attackCount < data.AttackCount; attackCount++)
            {
                for (int i = 0; i < data.status.Length; i++)
                {
                    colect_ = data.status[i].aoeCollect;
                    eaStruct = data.status[i];
                    var getAttack = pool_.GetObject(colect_);
                    getAttack.IsActive(eaStruct);
                    attackWait = new WaitForSeconds(eaStruct.nextAttackTime);

                    yield return attackWait;
                }
            }
            attackPhase++;
        }
    }
}