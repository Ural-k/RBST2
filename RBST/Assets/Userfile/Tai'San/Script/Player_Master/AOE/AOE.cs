using System.Drawing;
using UnityEngine;

public class AOE : MonoBehaviour
{
    private Mesh mesh_;
    private MeshFilter meshFilter_;

    public int segments = 32; // 円・扇の滑らかさ

    /// <summary>
    /// 呼び出す瞬間のセットアップ
    /// </summary>
    public AOE Initialize()
    {
        mesh_ = new Mesh();
        meshFilter_ = GetComponent<MeshFilter>();
        meshFilter_.mesh = mesh_;

        return this;
    }

    //private void ActiveFalse()

    /// <summary>
    /// 円または扇のAOE
    /// </summary>
    /// <param name="angleDeg">内角</param>
    /// <param name="dir">向き</param>
    /// <param name="radius">半径</param>
    public void BuildFan(Vector2 center, float angleDeg, Vector2 dir, float radius)
    {
        int segCount = angleDeg >= 360f ? segments : Mathf.Max(2, (int)(segments * (angleDeg / 360f)));
        Vector3[] verts = new Vector3[segCount + 2];
        Vector2[] uvs = new Vector2[segCount + 2];
        int[] tris = new int[segCount * 3];

        verts[0] = center;
        uvs[0] = new Vector2(0.5f, 0.5f); // 中心のUV

        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float startAngle = baseAngle - angleDeg / 2f;

        for (int i = 0; i <= segCount; i++)
        {
            float t = startAngle + angleDeg * (i / (float)segCount);
            float rad = t * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
            verts[i + 1] = offset + (Vector3)center;

            // 半径で正規化してから 0〜1 にマッピング
            uvs[i + 1] = new Vector2(
                Mathf.Cos(rad) * 0.5f + 0.5f,
                Mathf.Sin(rad) * 0.5f + 0.5f
            );
        }

        for (int i = 0; i < segCount; i++)
        {
            tris[i * 3] = 0;
            tris[i * 3 + 1] = i + 1;
            tris[i * 3 + 2] = i + 2;
        }

        mesh_.Clear();
        mesh_.vertices = verts;
        mesh_.uv = uvs;
        mesh_.triangles = tris;
        mesh_.RecalculateNormals();
    }

    public void BuildCircle(Vector2 center, Vector2 radius)
    {
        Vector3[] verts = new Vector3[segments + 1];
        Vector2[] uvs = new Vector2[segments + 1];
        int[] tris = new int[segments * 3];

        verts[0] = center;
        uvs[0] = new Vector2(0.5f, 0.5f); // 中心のUV

        for (int i = 0; i < segments; i++)
        {
            float rad = 360f * i / segments * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad) * radius.x, Mathf.Sin(rad) * radius.y, 0);
            verts[i + 1] = offset + (Vector3)center;

            // -1〜1 の範囲を 0〜1 にマッピング
            uvs[i + 1] = new Vector2(
                Mathf.Cos(rad) * 0.5f + 0.5f,
                Mathf.Sin(rad) * 0.5f + 0.5f
            );
        }

        for (int i = 0; i < segments; i++)
        {
            int next = (i + 1) % segments + 1;
            tris[i * 3] = 0;
            tris[i * 3 + 1] = i + 1;
            tris[i * 3 + 2] = next;
        }

        mesh_.Clear();
        mesh_.vertices = verts;
        mesh_.uv = uvs; // ← 追加
        mesh_.triangles = tris;
        mesh_.RecalculateNormals();
    }

    /// <summary>
    /// 矩形のAOE
    /// </summary>
    /// <param name="width">横</param>
    /// <param name="height">縦</param>
    public void BuildRectangle(Vector2 center, float width, float height, float rectAngle)
    {
        Vector3[] localVerts = new Vector3[4]
        {
            new Vector3(-width/2, -height/2, 0),
            new Vector3( width/2, -height/2, 0),
            new Vector3( width/2,  height/2, 0),
            new Vector3(-width/2,  height/2, 0),
        };

        Vector2[] uvs = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
        };

        Quaternion rot = Quaternion.Euler(0, 0, rectAngle);
        Vector3[] verts = new Vector3[4];
        for (int i = 0; i < 4; i++)
            verts[i] = (Vector3)center + rot * localVerts[i];

        int[] tris = { 0, 1, 2, 0, 2, 3 };
        mesh_.Clear();
        mesh_.vertices = verts;
        mesh_.uv = uvs;
        mesh_.triangles = tris;
        mesh_.RecalculateNormals();
    }

    /// <summary>
    /// ドーナツ範囲
    /// </summary>
    /// <param name="innerRadius"></param>
    /// <param name="radius"></param>
    public void BuildDonut(Vector2 center, float innerRadius, float radius)
    {
        Vector3[] verts = new Vector3[(segments + 1) * 2];
        Vector2[] uvs = new Vector2[(segments + 1) * 2];
        int[] tris = new int[segments * 6];

        for (int i = 0; i <= segments; i++)
        {
            float rad = (360f * i / segments) * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
            verts[i * 2] = (Vector3)center + dir * innerRadius;
            verts[i * 2 + 1] = (Vector3)center + dir * radius;

            // 角度をU、内側/外側をVに割り当てる
            float u = i / (float)segments;
            uvs[i * 2] = new Vector2(u, 0f);     // 内側リング
            uvs[i * 2 + 1] = new Vector2(u, 1f); // 外側リング
        }

        for (int i = 0; i < segments; i++)
        {
            int i0 = i * 2;
            int i1 = i * 2 + 1;
            int i2 = i * 2 + 2;
            int i3 = i * 2 + 3;

            tris[i * 6] = i0;
            tris[i * 6 + 1] = i2;
            tris[i * 6 + 2] = i1;

            tris[i * 6 + 3] = i1;
            tris[i * 6 + 4] = i2;
            tris[i * 6 + 5] = i3;
        }

        mesh_.Clear();
        mesh_.vertices = verts;
        mesh_.uv = uvs;
        mesh_.triangles = tris;
        mesh_.RecalculateNormals();
    }
}


