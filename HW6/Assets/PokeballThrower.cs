using UnityEngine;

public class PokeballThrower : MonoBehaviour
{
    [Header("拋擲參數")]
    public float throwForceZ = 500f;
    public float throwForceY = 300f;
    public float delayTime = 1f;
    [Header("特效與音效")]
    public GameObject catchEffectPrefab;
    public AudioClip catchSound;
    private Rigidbody rb;
    private AudioSource audioSource;
    private bool hasThrown = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        Invoke("StartThrow", delayTime);
    }
    void StartThrow()
    {
        if (hasThrown || rb == null) return;
        rb.isKinematic = false;
        Vector3 finalForceVector = new Vector3(0, throwForceY, throwForceZ);
        rb.AddForce(finalForceVector);
        hasThrown = true;
        Debug.Log("精靈球已拋出！");
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pokemon"))
        {
            CatchPokemon(collision.gameObject);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    void CatchPokemon(GameObject pokemon)
    {
        if (audioSource != null && catchSound != null)
        {
            audioSource.PlayOneShot(catchSound);
        }
        if (catchEffectPrefab != null)
        {
            Instantiate(catchEffectPrefab, this.transform.position, Quaternion.identity);
        }
        pokemon.SetActive(false);

        Debug.Log("收服成功！寶可夢已被收服並隱藏。");
    }
}