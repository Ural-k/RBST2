using UnityEngine;


public class BottleController : MonoBehaviour
{


    public float rotateSpeed = 720f;



    void Update()
    {

        transform.Rotate(
        0,
        0,
        rotateSpeed *
        Time.deltaTime);

    }


}