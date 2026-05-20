using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class AOEControll : MonoBehaviour
{
    [SerializeField] private GameObject entity_; //AOEの実体
    [SerializeField] private GameObject entry_;  //AOEの予兆
    [SerializeField] private float entryTime_;   //予兆時間
    [SerializeField] private float entityTime_;  //実体時間
    [SerializeField] private int damage_;        //ダメージ量
    [SerializeField] private float diameter_;    //範囲(直径）
    [SerializeField] private AOEShapeWrapper shapeWrapper_; //AOEWrapperのインスタンス


    private IAOEshape shape_;                    //範囲の形状
    
    private Vector3 pos_;                        //範囲生成位置
    private float timer_;                        //経過時間
    private float innerRaito_;                   //内側の円が
    private bool warningActiveFlag_;             //起動フラグ
    private bool entityActiveFlag_;              //実体起動フラグ
    

    //プロパテぃ

    public int Damage { get { return damage_; } set { damage_ = value; } }

    public float Radius { get { return diameter_ / 2; } }

    private Vector2 Scale { get { return new Vector2(diameter_, diameter_); } }
    

    private void Awake()
    {
        //初期化
        entity_.SetActive(false);
        entry_.SetActive(false);
        timer_ = entryTime_;
        innerRaito_ = 0.0f;
        warningActiveFlag_ = false;
        entityActiveFlag_ = false;
        pos_ = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        pos_ = transform.position;
        Entry();
        Entity();

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
    public void IsActive(IAOEshape shape, float scale, float innerRaito)
    {
        //生成されたときのステータスを設定
        shape_ = shape;
        diameter_ = scale;
        transform.localScale = Scale;
        innerRaito_ = innerRaito;

        //判定の開始
        if (entityActiveFlag_) { return; }
        if (warningActiveFlag_) { return; }
        else{ warningActiveFlag_ = true; }

    }

    /// <summary>
    /// 予兆を生成一定時間後に実体に移行
    /// </summary>

    private  void Entry()
    {
        
        //entriyActiveFlagがfalseなら起動しない
        if (!warningActiveFlag_) { return; }

        
        entry_.SetActive (true);
        timer_ -= Time.deltaTime;
        if (timer_ < 0)
        {
            entry_.SetActive(false);
            warningActiveFlag_ = false;
            entityActiveFlag_ = true;
            timer_ = entityTime_;
        }

    }

    /// <summary>
    /// 実体を生成一定時間後に使った変数をリセット
    /// </summary>
    private void Entity()
    {
        //entityActiveFlagがfalseなら起動しない
        if (!entityActiveFlag_) { return; }
        
        entity_ .SetActive (true);
        timer_ -= Time .deltaTime;
        ApplyDamage();
        if (timer_ < 0)
        {
            
            entity_.SetActive(false);
            entityActiveFlag_ = false;
            timer_ = entityTime_;

            //オブジェクトプールに変更するとき削除予定
            Destroy(gameObject);
        }
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
}