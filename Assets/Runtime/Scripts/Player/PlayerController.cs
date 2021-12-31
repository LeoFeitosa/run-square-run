using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(ParticleSystem))]
public class PlayerController : MonoBehaviour
{
    PlayerMovementController playerMovement;
    ParticleSystem particles;
    SpriteRenderer[] spriteRenderer;
    BoxCollider2D boxCollider2D;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovementController>();
        particles = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Die();
            particles.Play();
        }
    }

    void Die()
    {
        boxCollider2D.enabled = false;

        foreach (var render in spriteRenderer)
        {
            render.enabled = false;
        }

        playerMovement.StopMove();
    }
}
