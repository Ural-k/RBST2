using Unity.VisualScripting;
using UnityEngine;

public class TestEnemyControll : MonoBehaviour
{
    [SerializeField] private GameObject cricle_;
    [SerializeField] private GameObject square_;
    [SerializeField] private Vector2 positionAOE_;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var cricle = Instantiate(cricle_,positionAOE_,Quaternion.identity);
            cricle.GetComponent<AOEControll>().isActive();
        }

        if (Input.GetMouseButtonDown(1))
        {
            var square = Instantiate(square_, positionAOE_, Quaternion.identity);
            square.GetComponent<AOEControll>().isActive();
        }
    }
}
