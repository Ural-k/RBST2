using Unity.VisualScripting;
using UnityEngine;

public class TestEnemyControll : MonoBehaviour
{
    [SerializeField] private GameObject[] debug_;
    [SerializeField] private GameObject square_;
    [SerializeField] private Vector2 positionAOE_;
    [SerializeField] private float scale_;

    [SerializeField] private AOECollect colect_;

    void Start()
    {
        //shapeWrapper_.AOESet();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            int i = Random .Range(0,PlayerManager.GetAllPlayerListCount());
            if(PlayerManager.GetPlayer(i) == null)
            {
                return;
            }
            Player player = PlayerManager.GetPlayer(i);

            positionAOE_ = player.GetComponent<Transform>().transform.position;

            var debug = Instantiate(debug_[(int)colect_], positionAOE_, Quaternion.identity);
            //debug.GetComponent<AOEControll>().IsActive(shapeWrapper_.CallAOE(colect_),scale_,innerRadius_);
        }
    }
}
