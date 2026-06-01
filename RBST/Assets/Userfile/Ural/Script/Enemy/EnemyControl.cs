using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private EnemyAtackObjectPool pool_;

    [SerializeField]private AOECollect colect_;
    private TransformStract tfStruct;

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
    }

    IEnumerator GimmickCorutine()
    {
        while (true)
        {
            //入場
            if (!entryFlag_)
            {
                //後々追加
            }
            //待機
            yield return new WaitForSeconds(waitTime);

            //攻撃
            //攻撃のステータスを設定（すくたぶがいいなぁ（ちらちら）
            tfStruct.pos = new Vector2(0,0);    //攻撃を出す座標
            tfStruct.innerRadius = 0f;          //内側の円の半径（ドーナツ使用時以外０）
            tfStruct.scale = 2f;                //攻撃のサイズ
            //↓ここだけ必須
            colect_ = AOECollect.Circle;        //攻撃の種類を設定
            //攻撃の表示と再生
            var GetAttack = pool_.GetObject(colect_);
            GetAttack.IsActive(tfStruct);

            yield return new WaitForSeconds(waitTime);

            colect_ = AOECollect.Box;
            tfStruct.pos = new Vector2(0, 30);
            tfStruct.scale = 55;

            var uenagiharai = pool_.GetObject(colect_);
            uenagiharai.IsActive(tfStruct);

            colect_ = AOECollect.Box;
            

            //↑　好きに改造してね♡
            yield return new WaitForSeconds(waitTime);
            //移動

            //なんかもうがんばれ
            yield return new WaitForSeconds(waitTime);
        }
    }
}

