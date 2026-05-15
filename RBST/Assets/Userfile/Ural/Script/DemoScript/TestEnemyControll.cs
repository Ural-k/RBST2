using Unity.VisualScripting;
using UnityEngine;

public class TestEnemyControll : MonoBehaviour
{
    [SerializeField] private GameObject debug_;
    [SerializeField] private GameObject square_;
    [SerializeField] private Vector2 positionAOE_;
    [SerializeField] private AOEShapeWrapper shapeWrapper_;

    [SerializeField] private AOEColect colect_;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //var cricle = Instantiate(cricle_,positionAOE_,Quaternion.identity);
            //cricle.GetComponent<AOEControll>().isActive();

            //var square = Instantiate(square_, positionAOE_, Quaternion.identity);
            //square.GetComponent<AOEControll>().isActive();

            int i = Random .Range(0,PlayerManager.GetAllPlayerListCount());
            if(PlayerManager.GetPlayer(i) == null)
            {
                return;
            }
            Player player = PlayerManager.GetPlayer(i);

            positionAOE_ = player.GetComponent<Transform>().transform.position;

            var debug = Instantiate(debug_, positionAOE_, Quaternion.identity);
            debug.GetComponent<AOEControll>().IsActive(shapeWrapper_.CallAOE(colect_));
        }
    }
}
