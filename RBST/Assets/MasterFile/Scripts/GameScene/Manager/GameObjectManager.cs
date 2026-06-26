using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab_;
    [SerializeField] private GameObject enemyPrefab_;

    public static GameObjectManager Instance;

    //デバッグ用
    private Vector2 offset = new Vector2(5, 0);


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void CreatePlayer()
    {
        Instantiate(playerPrefab_).GetComponent<Player>();
    }
    public EnemyControl CreateEnemy()
    {
        var temp = Instantiate(enemyPrefab_,offset,Quaternion.identity);
        EnemyControl enemy = temp.GetComponent<EnemyControl>();
        EnemyManager.AddEnemy(enemy);

        return enemy;
    }
    public void DestroyEnemy(EnemyControl enemyControl)
    {
        EnemyManager.DeleteEnemy(enemyControl);
    }
}
