using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(ParticleSystem))]
public class PlayerController : MonoBehaviour
{
    GameOverController gameOver;
    PlayerMovementController playerMovement;
    ParticleSystem particles;
    SpriteRenderer[] spriteRenderer;
    BoxCollider2D boxCollider2D;
    [Header("SFX")]
    [SerializeField] AudioClip sxfDie;

    void Awake()
    {
        gameOver = FindObjectOfType<GameOverController>();
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
        PlaySfxDie();

        gameOver.GetComponent<Canvas>().enabled = true;
        boxCollider2D.enabled = false;

        DisableSpriteRenderer();

        playerMovement.StopMove();
    }

    void DisableSpriteRenderer()
    {
        foreach (var render in spriteRenderer)
        {
            render.enabled = false;
        }
    }

    void PlaySfxDie()
    {
        if (sxfDie)
        {
            AudioController.Instance.PlayAudioCue(sxfDie);
        }
    }
}
