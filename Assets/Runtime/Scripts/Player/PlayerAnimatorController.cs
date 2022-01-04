using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    PlayerMovementController player;
    Animator anim;
    void Awake()
    {
        player = GetComponent<PlayerMovementController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimRotate();
    }

    void AnimRotate()
    {
        if (player.IsJumping)
        {
            anim.SetTrigger("rotate");
        }

        anim.SetBool("run", player.IsRun);
    }
}
