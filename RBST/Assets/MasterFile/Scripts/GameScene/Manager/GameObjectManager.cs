using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab_;
    [SerializeField] private GameObject enemyPrefab_;

    public static GameObjectManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void CreatePlayer()
    {
        Player player = Instantiate(playerPrefab_).GetComponent<Player>();
        PlayerManager.AddPlayer(player);
    }
    public EnemyControl CreateEnemy()
    {
        var temp = Instantiate(enemyPrefab_);
        EnemyControl enemy = temp.GetComponent<EnemyControl>();
        EnemyManager.AddEnemy(enemy);

        return enemy;
    }
    public void DestroyEnemy(EnemyControl enemyControl)
    {
        EnemyManager.DeleteEnemy(enemyControl);
    }
}
