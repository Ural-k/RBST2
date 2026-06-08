using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab_;
    [SerializeField] private GameObject enemyPrefab_;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CreatePlayer();
            CreateEnemy();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //DestroyEnemy();
            GameSceneManager.Instance.GameOver();
        }
    }

    private void CreatePlayer()
    {
        Player player = Instantiate(playerPrefab_).GetComponent<Player>();
        PlayerManager.AddPlayer(player);
    }
    private void CreateEnemy()
    {
        EnemyControl enemy = Instantiate(enemyPrefab_).GetComponent <EnemyControl>();
        EnemyManager.AddEnemy(enemy);
    }
    private void DestroyEnemy(EnemyControl enemyControl)
    {
        EnemyManager.DeleteEnemy(enemyControl);
        
    }
}
