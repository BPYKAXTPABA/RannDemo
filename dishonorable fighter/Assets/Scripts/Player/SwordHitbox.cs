using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    [SerializeField] private float swordDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(swordDamage);
        }
    }
}
