using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class AttackTimelineEntry
{
    public float triggerTime;      // 開始からの経過秒数
    public EnemyAttackBase attackData;
}

public class EnemyTimeline : MonoBehaviour
{
    [SerializeField] private List<AttackTimelineEntry> timeline = new();

    private void Start() => StartCoroutine(RunTimeline());

    private IEnumerator RunTimeline()
    {
        while (true)
        {
            var sorted = timeline.OrderBy(e => e.triggerTime).ToList();
            float elapsed = 0f;
            foreach (var entry in sorted)
            {
                float wait = entry.triggerTime - elapsed;
                if (wait > 0) yield return new WaitForSeconds(wait);
                entry.attackData.Execute(transform.position, transform);
                elapsed = entry.triggerTime;
            }
            yield return null;
        }
    }
}
