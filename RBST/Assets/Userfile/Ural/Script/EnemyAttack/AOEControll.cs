using Unity.VisualScripting;
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


    private IAOEshape shape_;                    //範囲の形状
    private Vector3 pos_;                        //範囲生成位置
    private float timer_;                        //経過時間
    private float innerRadius_;
    private bool warningActiveFlag_;             //起動フラグ
    private bool entityActiveFlag_;              //実体起動フラグ
    

    //プロパティ
    public float EntryTime { get { return entryTime_; } }

    public float EntityTime { get { return entityTime_; } }

    public int Damage { get { return damage_; } set { damage_ = value; } }

    public bool WarningActiveFlag { get { return warningActiveFlag_; }  set {  warningActiveFlag_ = value; } }

    public bool EntityActiveFlag { get { return entityActiveFlag_; } set { entityActiveFlag_ = value; } }

    public float Radius { get { return diameter_ / 2; } }
    

    private void Awake()
    {
        entity_.SetActive(false);
        entry_.SetActive(false);
        timer_ = entryTime_;
        innerRadius_ = 0.0f;
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
    }

    /// <summary>
    /// 予兆を生成するフラグをオンにする
    /// のちのち引数をscriptableに変更予定
    /// </summary>
    public void IsActive(IAOEshape shape, Vector3 scale, float innerRadius)
    {

        shape_ = shape;
        transform.localScale = scale;
        diameter_ = scale.x;
        innerRadius_ = innerRadius;
        if (entityActiveFlag_) { return; }
        if (WarningActiveFlag) { return; }
        else{ WarningActiveFlag = true; }

    }

    /// <summary>
    /// 予兆を生成一定時間後に実体に移行
    /// </summary>

    private  void Entry()
    {
        
        //entriyActiveFlagがfalseなら起動しない
        if (!WarningActiveFlag) { return; }

        
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
            Debug.Log("A");
            entity_.SetActive(false);
            entityActiveFlag_ = false;
            timer_ = entityTime_;

            Destroy(gameObject);
        }
    }


    /// <summary>
    /// ダメージ処理
    /// </summary>
    private void ApplyDamage()
    {
        if(shape_.AOEColect == AOEColect.Donut)
        {
            InnerRadiusSet();
        }
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
        shape_.InnerRadius = innerRadius_;

    }

    private void OnDrawGizmos()
    {
        shape_.OnDrawGizmos(Radius, pos_);
    }
}

