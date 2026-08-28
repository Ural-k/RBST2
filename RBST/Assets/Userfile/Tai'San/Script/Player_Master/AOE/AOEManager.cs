using UnityEngine;

public class AOEManager : MonoBehaviour
{
    public static AOEManager Instance { get; private set; } = null;

    [SerializeField] private GameObject aoePrefab_;
    private UnityEngine.Pool.ObjectPool<AOE> pool_;

    private void Awake()
    {
        if (Instance) return;

        Instance = this;
        Instance.pool_ = new UnityEngine.Pool.ObjectPool<AOE>(
                () => Instantiate(aoePrefab_, transform).GetComponent<AOE>().Initialize(),
                aoe => aoe.gameObject.SetActive(true),
                aoe => aoe.gameObject.SetActive(false),
                aoe => Destroy(aoe.gameObject)
            );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) { ShowRectangle(Vector2.zero, new Vector2(20, 2), 45); }
    }

    public void ShowCircle(Vector2 center, Vector2 radius)
    {
        var aoe = pool_.Get();
        aoe.BuildCircle(center, radius);
    }

    public void ShowRectangle(Vector2 center, Vector2 wh, float rectAngle = 0)
    {
        var aoe = pool_.Get();
        aoe.BuildRectangle(center, wh.x, wh.y, rectAngle);
    }

    public void ShowDonut(Vector2 center, float innerRadius, float radius)
    {
        var aoe = pool_.Get();
        aoe.BuildDonut(center, innerRadius, radius);
    }

    public void ShowFan(Vector2 center, float angleDeg, float radius)
    {
        var aoe = pool_.Get();
        aoe.BuildFan(center, angleDeg, Vector2.right, radius);
    }
}
