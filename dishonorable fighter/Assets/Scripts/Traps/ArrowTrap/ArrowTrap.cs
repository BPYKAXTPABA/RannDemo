using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;      
    [SerializeField] private Transform firePoint;            
    [SerializeField] private GameObject[] arrows;            
    [SerializeField] private float arrowSpeed;          
    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        cooldownTimer = 0;

        GameObject arrow = GetArrow();
        if (arrow != null)
        {
            arrow.transform.position = firePoint.position;
            arrow.SetActive(true);
            SoundManager.instance.PlaySound(arrowSound);
            arrow.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * arrowSpeed;
        }
    }

    private GameObject GetArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return arrows[i];
        }
        return null; 
    }

    public void ResetTrap()
    {
        cooldownTimer = 0f;

        for (int i = 0; i < arrows.Length; i++)
        {
            if (arrows[i] != null)
            {
                arrows[i].SetActive(false);
                arrows[i].transform.position = firePoint.position;

                // to make arrow possible activated as a new one
                Rigidbody2D rb = arrows[i].GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.linearVelocity = Vector2.zero;
            }
        }
    }

}
