using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ShakeScreenController : MonoBehaviour
{
    public static ShakeScreenController Instance;
    Transform cameraTransform;
    bool shakeStart;
    [SerializeField] float shakeDuration = 0;
    [SerializeField] float shakeMagnitude = 0.7f;

    // A posição inicial do 
    Vector3 initialPosition;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }
    }

    void Start()
    {
        shakeStart = false;
    }

    void Update()
    {
        if (shakeStart)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            StartCoroutine(CountShakeDuration());
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    IEnumerator CountShakeDuration()
    {
        yield return new WaitForSeconds(shakeDuration);
        shakeStart = false;
    }

    public void ShakeNow()
    {
        shakeStart = true;
    }
}
