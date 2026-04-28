using Unity.VisualScripting;
using UnityEngine;

public class AOEControll : MonoBehaviour
{
    [SerializeField] private GameObject entity_; //AOEの実体
    [SerializeField] private GameObject entry_;  //AOEの予兆
    [SerializeField] private float entryTime_;   //予兆時間
    [SerializeField] private float entityTime_;  //実体時間
    [SerializeField] private int damage_;        //ダメージ量
    [SerializeField] private Vector3 scale_;   　//範囲
    private float timer_;                        //経過時間
    private bool entryActiveFlag_;               //起動フラグ
    private bool entityActiveFlag_;              //実体起動フラグ

    //プロパティ
    public float EntryTime { get { return entryTime_; } }

    public float EntityTime { get { return entityTime_; } }

    public int Damage { get { return damage_; } set { damage_ = value; } }

    public float Timer { get { return timer_; } set { timer_ = value; } }
    public bool EntryActiveFlag { get { return entryActiveFlag_; } set { entityActiveFlag_ = value; } }

    public bool EntityActiveFlag { get { return entityActiveFlag_; } set { entityActiveFlag_ = value; } }

    public Vector3 Scale { get { return scale_; }  set { scale_ = value; }  }
    

    void Start()
    {
        entity_.SetActive(false);
        entry_.SetActive(false);
        Timer = entryTime_;
        entryActiveFlag_ = false;
        entityActiveFlag_ = false;
        transform.localScale = scale_;
        isActive();
    }

    // Update is called once per frame
    void Update()
    {
        Entry();
        Entity();
    }

    /// <summary>
    /// 予兆を生成するフラグをオンにする
    /// </summary>
    public virtual void isActive()
    {
        if (entityActiveFlag_) { return; }
        if (entryActiveFlag_) { return; }
        else{ entryActiveFlag_ = true; }
    }

    /// <summary>
    /// 予兆を生成一定時間後に実体に移行
    /// </summary>

    public virtual void Entry()
    {
        //entriyActiveFlagがfalseなら起動しない
        if (!entryActiveFlag_) { return; }

        entry_.SetActive (true);
        timer_ -= Time.deltaTime;
        if (timer_ < 0)
        {
            entry_.SetActive(false);
            entryActiveFlag_ = false;
            entityActiveFlag_ = true;
            timer_ = entityTime_;
        }

    }

    /// <summary>
    /// 実体を生成一定時間後に使った変数をリセット
    /// </summary>
    public virtual void Entity()
    {
        //entityActiveFlagがfalseなら起動しない
        if (!entityActiveFlag_) { return; }

        entity_ .SetActive (true);
        timer_ -= Time .deltaTime;
        if (timer_ < 0)
        {
            entity_.SetActive(false);
            entityActiveFlag_ = false;
            timer_ = entityTime_;

            Destroy(gameObject);
        }
    }
}
