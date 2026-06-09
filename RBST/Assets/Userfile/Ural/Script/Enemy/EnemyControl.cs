using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private EnemyAtackObjectPool pool_;
    [SerializeField] private EnemyAttackPos[] scriptableObject_;

    private AOECollect colect_;
    private TransformStract tfStruct;
    private int attackPhase = 0;

    private int waitTime = 2;
    private bool entryFlag_;

    private void Awake()
    {
        entryFlag_ = false;
        StartCoroutine(GimmickCorutine());
    }

    void Start()
    {
        tfStruct = new TransformStract();
        tfStruct.pos = transform.position;
        tfStruct.innerRadius = 0f;
        tfStruct.scale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSceneManager.Instance.State == GameState.GameOver) { StopAllCoroutines(); }
    }

    /// <summary>
    /// 攻撃こルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator GimmickCorutine()
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
                tfStruct.pos = data.attackPos[i];
                tfStruct.scale = data.scale[i];
                var GetAttack = pool_.GetObject(colect_);
                GetAttack.IsActive(tfStruct);
            }
            attackPhase++;
            Debug.Log(attackPhase);
            yield return attackWait;

            ////移動
            //yield return new WaitForSeconds(waitTime);
        }
    }
}

