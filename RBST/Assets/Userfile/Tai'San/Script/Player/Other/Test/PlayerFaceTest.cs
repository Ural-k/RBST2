using UnityEngine;

public class PlayerFaceTest : MonoBehaviour
{
    Player player_;
    private void Start()
    {
        player_ = transform.parent.gameObject.GetComponent<Player>();
    }
    private void Update()
    {
        transform.localPosition = player_.GetInfo.lastFace_ / 2;
    }
}
