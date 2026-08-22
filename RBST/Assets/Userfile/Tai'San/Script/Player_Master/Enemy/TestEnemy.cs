using UnityEngine;

public class TestEnemy : MonoBehaviour, ITestTargetCircle
{
    public int hp_;

    Vector2 ITestTargetCircle.GetPosition => transform.position;

    public float Radius { get; set; } = 1;
    public Effect Effect { get; set; }

    void ITestTargetCircle.TakeDamage(int damage, ITestTargetCircle from)
    {
        hp_ -= damage;
    }

    void ITestTargetCircle.TakeHeal(int point, ITestTargetCircle from)
    {
        hp_ += point;
    }

    private void Start()
    {
        Effect = new(this);
    }
}
