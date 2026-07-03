using System.Collections.Generic;
using UnityEngine;

public class SkillIcom : MonoBehaviour
{
    [SerializeField] private Transform line_;
    [SerializeField] private List<GameObject> mainIcon_ = new List<GameObject>();
    [SerializeField] private List<GameObject> specialIcon_ = new List<GameObject>();
    [SerializeField] private List<GameObject> supportIcon_ = new List<GameObject>();
    private int main_, special_, support_;


    public void ChangeIconMain()
    {
        if(mainIcon_.Count - 1 == main_)
        {
            main_ = 0;
        }
        else
        {
            ++main_;
        }
    }
}
