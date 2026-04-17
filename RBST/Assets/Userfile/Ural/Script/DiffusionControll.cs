using UnityEngine;

public class DiffusionControll : AOEControll
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActive();
    }

    // Update is called once per frame
    void Update()
    {
        Entry();
        Entity();
    }

    public override void isActive()
    {
        base.isActive();
    }

    public override void Entry()
    {
        base.Entry();
    }

    public override void Entity()
    {
        
    }
}
