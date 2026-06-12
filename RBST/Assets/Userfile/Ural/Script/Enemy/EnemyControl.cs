using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private EnemyAtackObjectPool pool_;
    [SerializeField] private EnemyAttackPos[] scriptableObject_;
    [SerializeField] private TargetCircle targetCircle_;

    private AOECollect colect_;
    private EnemyAttackStract eaStruct;
    private int attackPhase = 0;

    private int waitTime = 2;
    private bool entryFlag_;
    private int maxHp_ = 10000;

    //のちのちついか
    //private int damage_ = 10;

    public int HP { get { return targetCircle_.hp_; } }

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
        eaStruct.scale = 1f;
    }

    public void Died()
    {
        Destroy(gameObject);
    }


    /// <summary>
    /// 攻撃こルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator GimmickCorutine()
    {
        var wait = new WaitForSeconds(waitTime);
        var attackWait = new WaitForSeconds(5);
        while (true)
        {
            //入場
            if (!entryFlag_)
            {
                //後々追加
            }
            //待機
            yield return wait;

            ////攻撃
            ////攻撃のステータスを設定（すくたぶがいいなぁ（ちらちら）
            //tfStruct.pos = new Vector2(0,0);    //攻撃を出す座標
            //tfStruct.innerRadius = 0f;          //内側の円の半径（ドーナツ使用時以外０）
            //tfStruct.scale = 2f;                //攻撃のサイズ
            ////↓ここだけ必須
            //colect_ = AOECollect.Circle;        //攻撃の種類を設定
            ////攻撃の表示と再生
            //var GetAttack = pool_.GetObject(colect_);
            //GetAttack.IsActive(tfStruct);

            yield return wait;

            
            if (scriptableObject_.Length <= attackPhase) { attackPhase = 0; }
            var data = scriptableObject_[attackPhase];
            for (int i = 0;i < data.attackPos.Length;i++) 
            {
                colect_ = data.aoeCollect[i];
                eaStruct.pos = data.attackPos[i];
                eaStruct.scale = data.scale[i];
                var GetAttack = pool_.GetObject(colect_);
                GetAttack.IsActive(eaStruct);
            }
            attackPhase++;
            yield return attackWait;

            ////移動
            //yield return new WaitForSeconds(waitTime);
        }
    }
}

