using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DounutAOE : AOEControll
{
    [SerializeField] private float innerDiameter_;  //内側の円の直径
    [SerializeField] private float outerDiameter_;  //外側の円の直径
    [SerializeField] private GameObject innerCricle_;  //内側の円のオブジェクト
    [SerializeField] private GameObject outerCricle_;  //外側の円のオブジェクト

    private Vector3 innerScale_;  //内側の円のスケール
    private Vector3 outerScale_;  //外側の円のスケール

    //半径を生成（内側の円）
    public float GetInnerRadian { get { return innerDiameter_ / 2; } }
    //半径を生成（外側の円）
    public float GetOuterRadian { get { return outerDiameter_ / 2; } }


    void Start()
    {
        //初期化
        Timer = EntryTime;
        Debug.Log(Timer);
        innerScale_ = new Vector3 (innerDiameter_, innerDiameter_, innerDiameter_);
        outerScale_ = new Vector3 (outerDiameter_, outerDiameter_, outerDiameter_);
        innerCricle_.transform.localScale = innerScale_;
        outerCricle_.transform.localScale = outerScale_;

    }

    // Update is called once per frame
    void Update()
    {
        Entry();
        Entity();
    }

    public override void isActive()
    {
        //継承元のisActiveを起動する
        base.isActive();
    }

    public override void Entry()
    {
        //entriyActiveFlagがfalseなら起動しない
        if (!EntryActiveFlag) { return; }
        
        innerCricle_.SetActive(true);
        outerCricle_.SetActive(true);
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            innerCricle_.SetActive(false);
            outerCricle_.SetActive(false);
            EntryActiveFlag = false;
            EntityActiveFlag = true;
        }
    }

    public override void Entity()
    {
        //entityActiveFlagがfalseなら起動しない
        if (!EntityActiveFlag) { return; }

        EntityActiveFlag = false;

        Destroy(gameObject);

    }
}
