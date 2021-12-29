using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimationState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        if (clips.Length > 0)
        {
            AnimatorClipInfo rotateClipInfo = clips[0];

            PlayerMovementController player = animator.transform.GetComponent<PlayerMovementController>();

            float multiplier = rotateClipInfo.clip.length / player.JumpDuration;
            animator.SetFloat("rotateMultiplier", multiplier);
        }
    }
}
