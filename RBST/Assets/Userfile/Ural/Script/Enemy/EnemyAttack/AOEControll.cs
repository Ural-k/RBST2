using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class AOEControll : NetworkBehaviour
{
    [SerializeField] private GameObject entity_;   //AOEの実体
    [SerializeField] private GameObject warning_;  //AOEの予兆
    [SerializeField] private float warningTime_;   //予兆時間
    [SerializeField] private float entityTime_;    //実体時間
    [SerializeField] private Vector2 scale_;
    [SerializeField] private AOECollect aoeCollect_;

    private AOEShapeWrapper shapeWrapper_;
    private IAOEshape shape_;                    //範囲の形状
    private Vector3 pos_;                        //範囲生成位置
    private float innerRaito_;                   //内側の円が
    private bool warningActiveFlag_;             //起動フラグ
    private bool entityActiveFlag_;              //実体起動フラグ
    private int damage_ = 250;                   //ダメージ量
    private EnemyAttackStract enemyAttackStract_;
    private float diameter_ { get { return enemyAttackStract_.scale.x; } }  //範囲(直径）

    //プロパテぃ
    public int Damage { get { return damage_; } set { damage_ = value; } }

    public float Radius { get { return diameter_ * 0.5f; } }

    public AOECollect ShapeColect { get { return aoeCollect_; } }

    public GameObject WarningObject { get { return warning_; } }
    public GameObject EntityObject { get { return entity_; } }
    

    private void Awake()
    {
        //初期化
        shapeWrapper_ = new AOEShapeWrapper();
        shape_ = shapeWrapper_.AOESet(aoeCollect_);
        warning_.SetActive(false);
        entity_.SetActive(false);
        innerRaito_ = 0.0f;
        warningActiveFlag_ = false;
        entityActiveFlag_ = false;
        pos_ = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        pos_ = transform.position;

        //マテリアルが_InnerRを持っていたら内側の範囲を設定する
        if (entity_.GetComponent<SpriteRenderer>().material.HasProperty("_InnerR"))
        {
            InnerRadiusSet();
        }
    }

    /// <summary>
    /// 予兆を生成するフラグをオンにする
    /// のちのち引数をscriptableに変更予定
    /// </summary>
    public void IsActive(EnemyAttackStract transformStruct )
    {
        //攻撃のステータスを受け取りステータスを設定する
        enemyAttackStract_ = transformStruct;
        SetStatus();

        //判定の開始
        if (entityActiveFlag_) { return; }
        if (warningActiveFlag_) { return; }
        else{ gameObject.SetActive(true); StartCoroutine(Coroutine()); }
    }

    /// <summary>
    /// ステータスをオブジェクトに設定
    /// </summary>
    private void SetStatus()
    {
        damage_ = enemyAttackStract_.damage;    
        transform.position = enemyAttackStract_.pos;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, enemyAttackStract_.angle);
        entity_.transform.localScale = enemyAttackStract_.scale;
        warning_.transform.localScale = enemyAttackStract_.scale;
        innerRaito_ = enemyAttackStract_.innerRadius;
        warningTime_ = enemyAttackStract_.warningTime;
        entityTime_ = enemyAttackStract_.entityTime;
    }

    /// <summary>
    /// AOE用のコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator Coroutine()
    {
        warningActiveFlag_ = true;
        warning_.SetActive(true);
        yield return new WaitForSeconds(warningTime_);
        
            warning_.SetActive(false);
            warningActiveFlag_ = false;
            entityActiveFlag_ = true;
        

        //entityActiveFlagがfalseなら起動しない
        if (!entityActiveFlag_) { yield break; }

        entity_.SetActive(true);
        ApplyDamage();

        yield return new WaitForSeconds(entityTime_);

            entity_.SetActive(false);
            gameObject.SetActive(false);
            entityActiveFlag_ = false;
    }

    /// <summary>
    /// 内側が空洞の場合に空洞のステータスを渡す
    /// </summary>
    private void InnerRadiusSet()
    {


        float innerRadius = 0.0f;

        //与えられた割合をmaterialのパラメーターのステータスに変換
        float temp = AOEShapeWrapper.DenomalizeInnerFloat(innerRaito_);

        entity_.GetComponent<SpriteRenderer>().material.SetFloat("_InnerR", temp);
        warning_.GetComponent<SpriteRenderer>().material.SetFloat("_InnerR", temp);

        //内側の半径の作成
        innerRadius = Radius * innerRaito_;
        enemyAttackStract_.innerRadius = innerRadius;

    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    private void ApplyDamage()
    {
        var hits = shape_.GetHits(enemyAttackStract_, pos_);
        foreach (var hit in hits)
        {
            var d = hit.GetComponent<IDamageable>();
            if (d != null)
            {
                d.TakeDamage(damage_);
            }
        }
    }
    /// <summary>
    /// エディター内での当たり判定の可視化
    /// </summary>
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Matrix4x4 matrix = transform.localToWorldMatrix;
        shape_.OnDrawGizmos(enemyAttackStract_, pos_,matrix);
    }
#endif
}