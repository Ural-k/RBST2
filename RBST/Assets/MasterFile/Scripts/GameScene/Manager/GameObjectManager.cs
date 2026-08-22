using UnityEngine;
using UnityEngine.UI;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab_;
    [SerializeField] private GameObject magic_;
    [SerializeField] private GameObject pharmacist_;
    [SerializeField] private GameObject sword_;
    [SerializeField] private GameObject enemy1_;
    [SerializeField] private GameObject enemy2_;
    [SerializeField] private GameObject enemy3_;
    [SerializeField] private Text bossName_;
    [SerializeField] private GameObject bgmObject_, bgm2_;

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
    public void CreateMagic()
    {
        Instantiate(magic_).GetComponent<Player>();
    }
    public void CreatePharmacist()
    {
        Player ins = Instantiate(pharmacist_).GetComponent<Player>();
        PlayerManager.AddPlayer(ins);
    }
    public void CreateSword()
    {
        Instantiate(sword_).GetComponent<Player>();
    }

    public EnemyControl CreateEnemy1()
    {
        var temp = Instantiate(enemy1_,offset,Quaternion.identity);
        EnemyControl enemy = temp.GetComponent<EnemyControl>();
        EnemyManager.AddEnemy(enemy);
        bossName_.text = "RED APPLE";
        bgmObject_.SetActive(true);

        return enemy;
    }

    public EnemyControl CreateEnemy2()
    {
        var temp = Instantiate(enemy2_, offset, Quaternion.identity);
        EnemyControl enemy = temp.GetComponent<EnemyControl>();
        EnemyManager.AddEnemy(enemy);
        bossName_.text = "GREEN APPLE";

        return enemy;
    }

    public EnemyControl CreateEnemy3()
    {
        var temp = Instantiate(enemy3_, offset, Quaternion.identity);
        EnemyControl enemy = temp.GetComponent<EnemyControl>();
        EnemyManager.AddEnemy(enemy);
        bossName_.text = "WHITE APPLE";
        bgmObject_.SetActive(false);
        bgm2_.SetActive(true);

        return enemy;
    }

    public void DestroyEnemy(EnemyControl enemyControl)
    {
        bossName_.text = "NEXT TO ENTER";
        EnemyManager.DeleteEnemy(enemyControl);
    }
}
