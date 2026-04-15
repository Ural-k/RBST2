using UnityEngine;

public class CricleAOEControll : MonoBehaviour
{
    [SerializeField] private GameObject entity_; //AOEの実体
    [SerializeField] private GameObject entry_;  //AOEの予兆
    [SerializeField] private float entryTime_;   //予兆時間
    [SerializeField] private float entityTime_;  //実体時間
    private float timer_;                       //経過時間
    private bool entryActiveFlag_;               //起動フラグ
    private bool entityActiveFlag_;              //実体起動フラグ
    

    void Start()
    {
        entity_.SetActive(false);
        entry_.SetActive(false);
        timer_ = entryTime_;
        entryActiveFlag_ = false;
        entityActiveFlag_ = false;
    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ用
        if (Input.GetMouseButtonDown(0)) 
        {
            isActive();
        }

        AOEEntry();
        AOEEntity();
    }

    /// <summary>
    /// 予兆を生成するフラグをオンにする
    /// </summary>
    public void isActive()
    {
        if (entityActiveFlag_) { return; }
        if (entryActiveFlag_) { return; }
        else{ entryActiveFlag_ = true; Debug.Log("s"); }
    }

    /// <summary>
    /// 予兆を生成一定時間後に実体に移行
    /// </summary>

    public void AOEEntry()
    {
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
    public void AOEEntity()
    {
        if (!entityActiveFlag_) { return; }
        entity_ .SetActive (true);
        timer_ -= Time .deltaTime;
        if (timer_ < 0)
        {
            entity_.SetActive(false);
            entityActiveFlag_ = false;
            timer_ = entityTime_;
        }
    }
}
