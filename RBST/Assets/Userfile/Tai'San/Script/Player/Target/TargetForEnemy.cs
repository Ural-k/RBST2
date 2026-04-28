using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetForEnemy : MonoBehaviour
{
    [SerializeField] GameObject targetObject_;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) Target();
    }

    /// <summary>
    /// É^Å[ÉQÉbÉg
    /// </summary>
    void Target()
    {
        List<Transform> target = new();
        for(int i = 0; i < targetObject_.transform.childCount; ++i)
        { target.Add(targetObject_.transform.GetChild(i)); }

        //ç∂è„óDêÊ
        var sortTarget = target.OrderBy(n => n.position.x).ThenByDescending(n => n.position.y).ToList();
    }
}
