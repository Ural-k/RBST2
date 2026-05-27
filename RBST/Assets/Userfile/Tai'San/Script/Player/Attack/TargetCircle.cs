using UnityEngine;

public class TargetCircle : MonoBehaviour, IToEnemyDamageAble
{
    public int hp_;
    public void DamageAble(int damage)
    {
        hp_ = Mathf.Max(hp_ - damage, 0);
        if (hp_ == 0)
        {
            Debug.Log($"{gameObject.name} Å® death");
            Destroy(gameObject);//âº
        }
    }

    //void OnPostRender()
    //{
    //    GL.Begin(GL.LINE_STRIP);

    //    for (int i = 0; i <= 32; i++)
    //    {
    //        float a = i / 32f * Mathf.PI * 2;

    //        GL.Vertex(new Vector3(
    //            Mathf.Cos(a),
    //            Mathf.Sin(a)
    //        ));
    //    }

    //    GL.End();
    //}
}
