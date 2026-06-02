using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab_;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        Player player = Instantiate(playerPrefab_).GetComponent<Player>();
        PlayerManager.AddPlayer(player);
    }
}
