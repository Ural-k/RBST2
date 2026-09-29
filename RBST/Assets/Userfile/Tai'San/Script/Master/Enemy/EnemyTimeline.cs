using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AttackTimelineEntry
{
    public float triggerTime_;      // 開始からの経過秒数
    public float burstTime_;        // 予兆後から何秒後に出るか
    public EnemyAttackBase attackData_;
}

public class EnemyTimeline : MonoBehaviour
{
    [SerializeField] private List<AttackTimelineEntry> timeline_ = new();

    [Header("移動のステータス(最初の一つ目は入場)")]
    [SerializeField] private EnemyMoveAsset[] moveStatus_;

    private EnemyMoveWrapper moveWrapper_;
    private IEnemyMove moves_;
    private int movePhase_ = 1;
    private bool entryFlag_;
    private int waitTime_ = 5;

    private void Awake()
    {
        entryFlag_ = false;
        moveWrapper_ = new EnemyMoveWrapper();
    }
    private void Start()=>StartCoroutine(MoveCorutine());


    private IEnumerator RunTimeline()
    { 
        float elapsed = 0f;
        var waitTime = new WaitForSeconds(0);
        var sorted = timeline_.OrderBy(e => e.triggerTime_).ToList();
        var LastTime = new WaitForSeconds(timeline_.First().triggerTime_ + timeline_.Last().burstTime_);
        while (true)
        {
            foreach (var entry in sorted)
            {
                float wait = entry.triggerTime_ - elapsed;
                waitTime = new WaitForSeconds(wait);
                if (wait > 0) yield return waitTime;
                Vector3 setPos = transform.position;
                Transform setTransform = transform;
                if(entry.burstTime_ > 0) entry.attackData_.TelegraphDuration(setPos, setTransform);
                StartCoroutine(AwakenAOE(entry, setPos, setTransform));
                elapsed = entry.triggerTime_;
            }
            yield return LastTime;
        }
    }

    private IEnumerator AwakenAOE(AttackTimelineEntry entry, Vector3 position, Transform transform)
    {
        yield return new WaitForSeconds(entry.burstTime_);
        entry.attackData_.Execute(position, transform);
    }

    public IEnumerator MoveCorutine()
    {
        //移動待機時間の初期化
        var moveWait = new WaitForSeconds(waitTime_);
        while (true)
        {
            //入場演出
            if (entryFlag_ == false)
            {
                var entryData = moveStatus_[0];
                EnemyMoveStract moveData = entryData.status[0];

                IEnemyMove move = moveWrapper_.MoveSet(moveData.moveCollect);

                if (move != null)
                {
                    yield return StartCoroutine(move.EnemyMoveColutine(transform, moveData));
                }
                moveWait = new WaitForSeconds(moveData.nextMoveTime);
                StartCoroutine(RunTimeline());
                yield return moveWait;
                entryFlag_ = true;
                
            }

            //中身がなかったらブレイク
            if (moveStatus_ == null) { yield break; }

            //最後の移動が終わったら繰り返す
            if (moveStatus_.Length <= movePhase_)
            {
                movePhase_ = 1;
            }

            //移動データの取得
            var data = moveStatus_[movePhase_];

            //設定された移動処理を順次行う
            for (int i = 0; i < data.status.Length; i++)
            {
                EnemyMoveStract moveData = data.status[i];

                IEnemyMove move = moveWrapper_.MoveSet(moveData.moveCollect);

                if (move != null)
                {
                    yield return StartCoroutine(move.EnemyMoveColutine(transform, moveData));
                }

                yield return new WaitForSeconds(moveData.nextMoveTime);
            }

            //次の移動へ
            movePhase_++;
        }
    }
}
