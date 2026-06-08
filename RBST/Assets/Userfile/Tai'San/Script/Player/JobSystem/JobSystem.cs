using UnityEngine;

/// <summary>
///ジョブ固有のスキル実装(α版は使わない可能性)
/// </summary>
[CreateAssetMenu(fileName = "JobSystem", menuName = "ScriptableObjects/Player/JobSystem")]
public class JobSystem : ScriptableObject
{
    [SerializeField] private GameObject machinistBot;

    /// <summary>
    /// ジョブ個性スキル取得
    /// </summary>
    /// <param name="jobNumber"></param>
    /// <returns></returns>
    public IJobSystem GetJobSystem(int jobNumber)
    {
        IJobSystem result = null;
        switch (jobNumber)
        {
            case 0: result = new Magicz(); break;
            case 1: result = new Machinist(); break;
        }
        return result;
    }

}

public interface IJobSystem
{
    public bool SkillTrigger();
}

public class Magicz : IJobSystem
{
    public bool SkillTrigger() { return true; }
}

public class Machinist : IJobSystem
{
    public bool SkillTrigger(/*Listバフ*/)
    {
        return true;
    }
}