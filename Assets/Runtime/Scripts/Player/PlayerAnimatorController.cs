using System.Collections;
using System.Collections.Generic;
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
        anim.SetBool("rotate", player.IsJumping);
    }
}
