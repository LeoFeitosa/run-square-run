using TMPro;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(SpriteRenderer))]
public class ObstacleController : MonoBehaviour
{
    ScoreController scoreController;
    ParticleSystem particles;
    BoxCollider2D boxCollider2D;
    SpriteRenderer[] spriteRenderer;

    [Header("Colliders")]
    [SerializeField] float raycastSize = 3;
    [SerializeField] Transform transformRaycast;

    [Header("Score")]
    [SerializeField] int score;
    [SerializeField] GameObject scorePopUpPrefab;
    [SerializeField] float forceImpulseScore = 1f;
    bool hitRaycast;
    bool visualExplosion;
    [Header("SFX")]
    [SerializeField] AudioClip[] explosions;

    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        scoreController = FindObjectOfType<ScoreController>();
    }

    void Start()
    {
        hitRaycast = false;
        visualExplosion = false;
        spriteRenderer[0].color = RandomColors();
        var main = particles.main;
        main.startColor = spriteRenderer[0].color;
    }

    void FixedUpdate()
    {
        CheckRaycast();
    }

    void LateUpdate()
    {
        VisualEffectExplosion();
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
                visualExplosion = true;

                boxCollider2D.enabled = false;

                DisableSpriteRenderer();
                PlayRandomSfxExplosion();
            }

            if (hitUp.collider)
            {
                score = score * 2;
            }
        }
    }

    void VisualEffectExplosion()
    {
        if (visualExplosion)
        {
            visualExplosion = false;
            ShakeScreen();
            SetTextInPopUp(score);
            particles.Play();
            scoreController.SetScore(score);
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
        return Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
    }

    void SetTextInPopUp(int scoreNumber)
    {
        GameObject scorePopUp = Instantiate(scorePopUpPrefab, transform.position, Quaternion.identity);
        scorePopUp.GetComponentInChildren<TextMeshPro>().text = scoreNumber.ToString();
        scorePopUp.GetComponent<Rigidbody2D>().AddForce(transform.up * forceImpulseScore, ForceMode2D.Impulse);
        Destroy(scorePopUp.gameObject, 1);
    }

    void PlayRandomSfxExplosion()
    {
        if (explosions.Length > 0)
        {
            AudioClip sxf = explosions[Random.Range(0, explosions.Length)];
            AudioController.Instance.PlayAudioCue(sxf);
        }
    }

    void ShakeScreen()
    {
        ShakeScreenController.Instance.ShakeNow();
    }
}
