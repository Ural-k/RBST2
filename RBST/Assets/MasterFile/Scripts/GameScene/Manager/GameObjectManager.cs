using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab_;
    [SerializeField] private GameObject enemyPrefab_;

    public static GameObjectManager Instance;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CreatePlayer();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //DestroyEnemy();
            GameSceneManager.Instance.GameOver();
        }
    }

    public void CreatePlayer()
    {
        Player player = Instantiate(playerPrefab_).GetComponent<Player>();
        PlayerManager.AddPlayer(player);
    }
    public EnemyControl CreateEnemy()
    {
        EnemyControl enemy = Instantiate(enemyPrefab_).GetComponent <EnemyControl>();
        EnemyManager.AddEnemy(enemy);

        return enemy;
    }
    public void DestroyEnemy(EnemyControl enemyControl)
    {
        EnemyManager.DeleteEnemy(enemyControl);
        
    }
}
