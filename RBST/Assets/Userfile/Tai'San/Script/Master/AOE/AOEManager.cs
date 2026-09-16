using UnityEngine;

public class AOEManager : MonoBehaviour
{
    public static AOEManager Instance { get; private set; } = null;
    [SerializeField] private GameObject aoePrefab_;
    private UnityEngine.Pool.ObjectPool<AOE> pool_;

    private const float DURATION = 0.6f;

    private void Awake()
    {
        if (Instance) return;

        Instance = this;
        Instance.pool_ = new UnityEngine.Pool.ObjectPool<AOE>(
                () => Instantiate(aoePrefab_, transform).GetComponent<AOE>().Initialize(),
                aoe =>
                {
                    aoe.gameObject.SetActive(true);
                    aoe.OnReleaseRequested += HandleReleaseRequested; // 購読
                },
                aoe =>
                {
                    aoe.OnReleaseRequested -= HandleReleaseRequested; // 解除
                    aoe.gameObject.SetActive(false);
                },
                aoe => Destroy(aoe.gameObject)
            );
    }

    private void HandleReleaseRequested(AOE aoe)
    {
        pool_.Release(aoe);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) { ShowRectangle(Vector2.zero, new Vector2(20, 2), 45,Color.aliceBlue); }
    }

    public void ShowCircle(Vector2 center, Vector2 radius, Color color, float telegraphDuration = 0)
    {
        var aoe = pool_.Get();
        aoe.ResetState(telegraphDuration, DURATION, color);
        aoe.BuildCircle(center, radius);
    }

    public void ShowRectangle(Vector2 center, Vector2 wh, float rectAngle, Color color, float telegraphDuration = 0)
    {
        var aoe = pool_.Get();
        aoe.ResetState(telegraphDuration, DURATION, color);
        aoe.BuildRectangle(center, wh.x, wh.y, rectAngle);
    }

    public void ShowDonut(Vector2 center, float innerRadius, float radius, Color color, float telegraphDuration = 0)
    {
        var aoe = pool_.Get();
        aoe.ResetState(telegraphDuration, DURATION, color);
        aoe.BuildDonut(center, innerRadius, radius);
    }

    public void ShowFan(Vector2 center, float angleDeg, Vector2 dir, float radius, Color color, float telegraphDuration = 0)
    {
        var aoe = pool_.Get();
        aoe.ResetState(telegraphDuration, DURATION, color);
        aoe.BuildFan(center, angleDeg, dir, radius);
    }
}
