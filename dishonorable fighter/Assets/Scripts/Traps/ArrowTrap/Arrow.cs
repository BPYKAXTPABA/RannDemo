using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float lifeTime; // after few second arrow will dissapear
    [SerializeField] private float damage;
    [SerializeField] private SpriteRenderer arrowSprite;

    private void Awake()
    {
        arrowSprite = GetComponent<SpriteRenderer>();
        arrowSprite.enabled = true;
    }

    private void OnEnable()
    {
        Invoke("Deactivate", lifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>()?.TakeDamage(damage);
        }

        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
