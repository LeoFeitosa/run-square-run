using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(SpriteRenderer))]
public class ObstacleController : MonoBehaviour
{
    ScoreController scoreController;
    ParticleSystem particles;
    BoxCollider2D boxCollider2D;
    SpriteRenderer[] spriteRenderer;
    [SerializeField] float raycastSize = 3;
    [SerializeField] Transform transformRaycast;
    [SerializeField] int score;
    [SerializeField] GameObject scorePopUpPrefab;
    [SerializeField] float forceImpulseScore = 1f;
    bool hitRaycast = false;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        scoreController = GetComponent<ScoreController>();
    }

    void Start()
    {
        spriteRenderer[0].color = RandomColors();
        var main = particles.main;
        main.startColor = spriteRenderer[0].color;
    }

    void Update()
    {
        CheckRaycast();
    }

    void CheckRaycast()
    {
        if (!hitRaycast)
        {
            RaycastHit2D hitUp = Physics2D.Raycast(transformRaycast.position, transform.TransformDirection(Vector2.up), raycastSize, LayerMask.GetMask("Player"));
            Debug.DrawRay(transformRaycast.position, transform.TransformDirection(Vector2.up) * raycastSize, Color.yellow);

            RaycastHit2D hitDown = Physics2D.Raycast(transformRaycast.position, transform.TransformDirection(Vector2.down), raycastSize, LayerMask.GetMask("Player"));
            Debug.DrawRay(transformRaycast.position, transform.TransformDirection(Vector2.down) * raycastSize, Color.yellow);

            if (hitUp.collider || hitDown.collider)
            {
                hitRaycast = true;

                boxCollider2D.enabled = false;

                DisableSpriteRenderer();
                particles.Play();

                SetTextInPopUp();

                scoreController = scoreController ? scoreController : FindObjectOfType<ScoreController>();
                scoreController.SetScore(score);
            }
        }
    }

    void DisableSpriteRenderer()
    {
        foreach (var render in spriteRenderer)
        {
            render.enabled = false;
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

    void SetTextInPopUp()
    {
        GameObject scorePopUp = Instantiate(scorePopUpPrefab, transform.position, Quaternion.identity);
        scorePopUp.GetComponentInChildren<TextMeshPro>().text = score.ToString();
        scorePopUp.GetComponent<Rigidbody2D>().AddForce(transform.up * forceImpulseScore, ForceMode2D.Impulse);
        Destroy(scorePopUp.gameObject, 1);
    }
}
