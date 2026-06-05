using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class EnemyAtackObjectPool : MonoBehaviour
{

    [SerializeField] private GameObject[] aoeObject_;  //範囲が入っている配列
    private Dictionary<AOECollect,ObjectPool<AOEControll>> aoeDic_ = new ();

    private int listSize_ = 4;

    public void Awake()
    {
        //AOEのオブジェクトの数分リストを作る
        if (aoeObject_.Length > aoeDic_.Count)
        {
            for (int i = 0; i < aoeObject_.Length; i++)
            {
                aoeDic_[aoeObject_[i].GetComponent<AOEControll>().ShapeColect]
                    = new ObjectPool<AOEControll>(listSize_, aoeObject_[i]);
            }
        }
    }

    public AOEControll GetObject(AOECollect aoeColect)
    {
       return aoeDic_[aoeColect].Get();
    }
}
