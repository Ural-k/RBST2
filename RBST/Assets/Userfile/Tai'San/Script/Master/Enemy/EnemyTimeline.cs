using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AttackTimelineEntry
{
    public float triggerTime_;      // 開始からの経過秒数
    public EnemyAttackBase attackData_;
}

public class EnemyTimeline : MonoBehaviour
{
    [SerializeField] private List<AttackTimelineEntry> timeline_ = new();

    private void Start() => StartCoroutine(RunTimeline());

    private IEnumerator RunTimeline()
    {
        while (true)
        {
            var sorted = timeline_.OrderBy(e => e.triggerTime_).ToList();
            float elapsed = 0f;
            foreach (var entry in sorted)
            {
                float wait = entry.triggerTime_ - elapsed;
                if (wait > 0) yield return new WaitForSeconds(wait);
                entry.attackData_.Execute(transform.position, transform);
                elapsed = entry.triggerTime_;
            }
            yield return null;
        }
    }
}
