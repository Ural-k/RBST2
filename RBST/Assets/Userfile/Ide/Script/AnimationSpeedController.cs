using UnityEngine;

public class AnimationSpeedController : MonoBehaviour
{
    public Animator animator;


    // スクリプトから変更する秒数

    public float walkAnimationTime = 1.5f;

    public float throwAnimationTime = 1.0f;



    void Start()
    {
        SetAnimationSpeed();
    }


    public void SetAnimationSpeed()
    {
        AnimationClip[] clips =
            animator.runtimeAnimatorController.animationClips;


        foreach (AnimationClip clip in clips)
        {

            if (clip.name == "Walk")
            {
                clip.frameRate =
                clip.length / walkAnimationTime;
            }


            if (clip.name == "Throw")
            {
                clip.frameRate =
                clip.length / throwAnimationTime;
            }

        }
    }
}