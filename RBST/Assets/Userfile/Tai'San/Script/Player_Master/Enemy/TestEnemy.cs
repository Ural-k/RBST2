using UnityEngine;

public class TestEnemy : MonoBehaviour, ITargetCircle
{

    Vector2 ITargetCircle.GetPosition => transform.position;

    public Parameter Parameter { get; }
    public float Radius { get; set; } = 1;
    public Effect Effect { get; set; }

    void ITargetCircle.TakeDamage(int damage, ITargetCircle from)
    {
        //Parameter.hp_ -= damage;
    }

    void ITargetCircle.TakeHeal(int point, ITargetCircle from)
    {
        //hp_ += point;
    }

    private void Start()
    {
        Effect = new(this);
    }
}
