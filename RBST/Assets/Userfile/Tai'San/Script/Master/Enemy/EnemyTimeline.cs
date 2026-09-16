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

    private void Start() => StartCoroutine(RunTimeline());

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
}
