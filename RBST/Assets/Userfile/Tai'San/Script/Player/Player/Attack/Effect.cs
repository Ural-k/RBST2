using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    IEnumerator EffectCoroutine()
    {

        yield return null;
    }
}

/*<<ENUM>>*/
public enum EnemyBuff
{
    Poison,

}

public enum PlayerBuff
{
    
}