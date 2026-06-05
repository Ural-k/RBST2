using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class AOEControll : MonoBehaviour
{
    [SerializeField] private GameObject entity_; //AOEの実体
    [SerializeField] private GameObject warning_;  //AOEの予兆
    [SerializeField] private float entryTime_;   //予兆時間
    [SerializeField] private float entityTime_;  //実体時間
    [SerializeField] private int damage_;        //ダメージ量
    [SerializeField] private float diameter_;    //範囲(直径）
    [SerializeField] private AOECollect aoeCollect_;

    private AOEShapeWrapper shapeWrapper_;
    private IAOEshape shape_;                    //範囲の形状
    private Vector3 pos_;                        //範囲生成位置
    private float innerRaito_;                   //内側の円が
    private bool warningActiveFlag_;             //起動フラグ
    private bool entityActiveFlag_;              //実体起動フラグ
    

    //プロパテぃ

    public int Damage { get { return damage_; } set { damage_ = value; } }

    public float Radius { get { return diameter_ / 2; } }

    private Vector2 Scale { get { return new Vector2(diameter_, diameter_); } }

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
    public void IsActive(TransformStract transformStruct )
    {
        //表示されたときの座標とサイズ等を設定
        diameter_ = transformStruct.scale;
        innerRaito_ = transformStruct.innerRadius;
        transform.position = transformStruct.pos;
        transform.localScale = Scale;

        //判定の開始
        if (entityActiveFlag_) { return; }
        if (warningActiveFlag_) { return; }
        else{ gameObject.SetActive(true); StartCoroutine(Coroutine()); }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    private void ApplyDamage()
    {
        var hits = shape_.GetHits(Radius, pos_);
        foreach (var hit in hits)
        {
            var d = hit.GetComponent<IDamageable>();
            if (d != null)
            {
                Debug.Log("A");
                d.TakeDamage(damage_);
            }
        }
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

        //内側の半径の作成
        innerRadius = Radius * innerRaito_;
        shape_.InnerRadius = innerRadius;

    }

    /// <summary>
    /// エディター内での当たり判定の可視化
    /// </summary>
    private void OnDrawGizmos()
    {
        shape_.OnDrawGizmos(Radius, pos_);
    }

    IEnumerator Coroutine()
    {
        warningActiveFlag_ = true;
        warning_.SetActive(true);
        yield return new WaitForSeconds(entryTime_);
        
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
}