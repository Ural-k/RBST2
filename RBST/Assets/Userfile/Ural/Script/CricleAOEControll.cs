using UnityEngine;

public class CricleAOEControll : MonoBehaviour
{
    [SerializeField] private GameObject entity_; //AOEの実体
    [SerializeField] private GameObject entry_;  //AOEの予兆
    [SerializeField] private float timer_;       //経過時間
    [SerializeField] private float entryTime_;   //予兆時間
    [SerializeField] private float entityTime_;  //実体時間
    private bool entryActiveFlag_;                    //起動フラグ
    private bool entityActiveFlag_;                   //実体起動フラグ
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
        if (Input.GetMouseButtonDown(0)) 
        {
            isActive();
        }
        AOEEntry();
        AOEEntity();
    }

    public void isActive()
    {
        if (entryActiveFlag_) { return; }
        else{ entryActiveFlag_ = true; }
    }

    public void AOEEntry()
    {
        if (!entryActiveFlag_) { return; }
        entry_.SetActive (true);
        timer_ -= Time.deltaTime;
        if (timer_ < 0)
        {
            entry_.SetActive(false);
            entityActiveFlag_ = true;
            entryActiveFlag_ = false;
            timer_ = entityTime_;
        }

    }

    public void AOEEntity()
    {
        if (!entityActiveFlag_) { return; }
        entity_ .SetActive (true);
        timer_ -= Time .deltaTime;
        if (timer_ < 0)
        {
            entity_.SetActive(false);
            entryActiveFlag_ = false;
            entityActiveFlag_ = false;
            timer_ = entityTime_;
        }
    }
}
