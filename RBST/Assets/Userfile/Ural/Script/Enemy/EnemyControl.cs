using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private EnemyAtackObjectPool pool_;

    [SerializeField]private AOECollect colect_;
    private TransformStract tfStruct;

    private void Awake()
    {
    }

    void Start()
    {
        
        tfStruct = new TransformStract();
        tfStruct.pos = transform.position;
        tfStruct.innerRadius = 0f;
        tfStruct.scale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            int i = Random.Range(0, PlayerManager.GetAllPlayerListCount());
            if (PlayerManager.GetPlayer(i) == null)
            {
                return;
            }
            Player player = PlayerManager.GetPlayer(i);
            tfStruct.pos = player.transform.position;
            var GetAttack = pool_.GetObject(colect_);
            GetAttack.IsActive(tfStruct);
        }
    }
}

