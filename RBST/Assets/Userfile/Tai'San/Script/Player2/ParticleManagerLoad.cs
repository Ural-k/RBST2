using UnityEngine;

public class ParticleManagerLoad : MonoBehaviour
{
    private void Awake()
    {
        ParticleManager.InstanceLoad();
        Destroy(gameObject);
    }
    
}
