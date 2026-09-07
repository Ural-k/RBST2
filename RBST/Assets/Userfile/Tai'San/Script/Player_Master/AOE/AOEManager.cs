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
        if (Input.GetKeyDown(KeyCode.G)) { ShowRectangle(Vector2.zero, new Vector2(20, 2), 45); }
    }

    public void ShowCircle(Vector2 center, Vector2 radius, float telegraphDuration = 0, float duration = 0.6f)
    {
        var aoe = pool_.Get();
        aoe.ResetState(telegraphDuration, duration);
        aoe.BuildCircle(center, radius);
    }

    public void ShowRectangle(Vector2 center, Vector2 wh, float rectAngle, float telegraphDuration = 0, float duration = 0.6f)
    {
        var aoe = pool_.Get();
        aoe.ResetState(telegraphDuration, duration);
        aoe.BuildRectangle(center, wh.x, wh.y, rectAngle);
    }

    public void ShowDonut(Vector2 center, float innerRadius, float radius, float telegraphDuration = 0, float duration = 0.6f)
    {
        var aoe = pool_.Get();
        aoe.ResetState(telegraphDuration, duration);
        aoe.BuildDonut(center, innerRadius, radius);
    }

    public void ShowFan(Vector2 center, float angleDeg, Vector2 dir, float radius, float telegraphDuration = 0, float duration = 0.6f)
    {
        var aoe = pool_.Get();
        aoe.ResetState(telegraphDuration, duration);
        aoe.BuildFan(center, angleDeg, dir, radius);
    }
}
