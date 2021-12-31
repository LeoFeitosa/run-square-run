using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(SpriteRenderer))]
public class ObstacleController : MonoBehaviour
{
    ParticleSystem particles;
    SpriteRenderer spriteRenderer;
    [SerializeField] float raycastSize = 3;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.color = RandomColors();
        var main = particles.main;
        main.startColor = spriteRenderer.color;
    }

    void Update()
    {
        CheckRaycast();
    }

    void CheckRaycast()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), raycastSize, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * raycastSize, Color.yellow);

        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), raycastSize, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * raycastSize, Color.yellow);

        if (hitUp.collider || hitDown.collider)
        {
            particles.Play();
        }
    }

    Color RandomColors()
    {
        return new Color(
            Random.Range(0f, 1f), //Red
            Random.Range(0f, 1f), //Green
            Random.Range(0f, 1f), //Blue
            1 //Alpha (transparency)
        );
    }
}
