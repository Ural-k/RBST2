using UnityEngine;

public class DemoGameObjectManager : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが四人以下ならプレイヤーを生成できる
        if (PlayerManager.GetAllPlayerListCount() < 4 && Input.GetKeyDown(KeyCode.Return))
        {
            Player player = Instantiate(playerPrefab);
            PlayerManager.AddPlayer(player);
        }


        //デバッグ用
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            int i = Random.Range(0, 4);
            Player player = PlayerManager.GetPlayer(i);
            
            Debug.Log(player.GetComponent<PlayerBase>().gameObject.name);
        }

    }
}
