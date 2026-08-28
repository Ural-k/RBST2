using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private EnemyAtackObjectPool pool_;
    [Header("攻撃のステータス")]
    [SerializeField] private EnemyAttackStatus[] atackStatus_;
    [Header("移動のステータス(最初の一つ目は入場)")]
    [SerializeField] private EnemyMoveAsset[] moveStatus_;
    [SerializeField] private int maxHp_ = 10000;
    [SerializeField] private int hp_ = 10000;

    private EnemyMoveWrapper moveWrapper_;　
    private AOECollect colect_;
    private EnemyAttackStract eaStruct_;
    private IEnemyMove　moves_;
    private int attackPhase_ = 0;
    private int movePhase_ = 1;　

    private int waitTime_ = 5;
    private bool entryFlag_;

    public int HP { get { return hp_; } }
    public int MaxHP { get { return maxHp_; } }

    private void Awake()
    {
        entryFlag_ = false;
        moveWrapper_ = new EnemyMoveWrapper();
        StartCoroutine(GimmickCorutine());
        StartCoroutine(MoveCorutine());
    }

    void Start()
    {
        eaStruct_ = new EnemyAttackStract();
        eaStruct_.pos = transform.position;
        eaStruct_.innerRadius = 0f;
        eaStruct_.scale = Vector2.one;
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
        GameObjectManager.Instance.DestroyEnemy(this);
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
    /// ダメージテキストの呼び出し
    /// </summary>
    private void ShowFloatingText(int value, FloatingTextType type)
    {
        if (DamageTextManager.Instance == null) return;

        DamageTextManager.Instance.Show(transform.position, value, type);
    }

    /// <summary>
    /// 攻撃のコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator GimmickCorutine()
    {
        //攻撃待機時間の初期化
        var attackWait = new WaitForSeconds(waitTime_);
        while (true)
        {
            yield return attackWait;

            //攻撃が最後まで行ったら繰り返す
            if (atackStatus_.Length <= attackPhase_) { attackPhase_ = 0; }

            //攻撃のデータを取得
            var data = atackStatus_[attackPhase_];

            //設定された攻撃回数分同じ攻撃を繰り返す
            for (int attackCount = 0; attackCount < data.AttackCount; attackCount++)
            {
                //対応する攻撃を一つずつ表示し、ステータスを渡して実行
                for (int i = 0; i < data.status.Length; i++)
                {
                    colect_ = data.status[i].aoeCollect;
                    eaStruct_ = data.status[i];
                    var getAttack = pool_.GetObject(colect_);
                    getAttack.IsActive(eaStruct_);

                    //設定された攻撃の待機時間分間隔をあける
                    attackWait = new WaitForSeconds(eaStruct_.nextAttackTime);
                    yield return attackWait;
                }
            }

            //次の攻撃へ
            attackPhase_++;
        }
    }

    public IEnumerator MoveCorutine()
    {
        //移動待機時間の初期化
        var moveWait = new WaitForSeconds(waitTime_);
        while (true)
        {
            //入場演出
            if(entryFlag_ == false)
            {
                var entryData = moveStatus_[0];
                EnemyMoveStract moveData = entryData.status[0];

                IEnemyMove move = moveWrapper_.MoveSet(moveData.moveCollect);

                if (move != null)
                {
                    yield return StartCoroutine(move.EnemyMoveColutine(transform, moveData));
                }
                moveWait =  new WaitForSeconds(moveData.nextMoveTime);
                yield return moveWait;
                entryFlag_ = true;
            }

            //中身がなかったらブレイク
            if (moveStatus_ == null) { yield break;}

            //最後の移動が終わったら繰り返す
            if (moveStatus_.Length <= movePhase_)
            {
                movePhase_ = 1;
            }

            //移動データの取得
            var data = moveStatus_[movePhase_];

            //設定された移動処理を順次行う
            for (int i = 0; i < data.status.Length; i++)
            {
                EnemyMoveStract moveData = data.status[i];

                IEnemyMove move = moveWrapper_.MoveSet(moveData.moveCollect);

                if (move != null)
                {
                    yield return StartCoroutine(move.EnemyMoveColutine(transform, moveData));
                }

                yield return new WaitForSeconds(moveData.nextMoveTime);
            }

            //次の移動へ
            movePhase_++;
        }
    }

}

