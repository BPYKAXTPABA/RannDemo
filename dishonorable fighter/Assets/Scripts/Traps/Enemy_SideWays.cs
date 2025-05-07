using System.Collections;
using UnityEngine;

public class Enemy_SideWays : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float disableTime = 0.5f;
    [SerializeField] private float movementDistance;
    [SerializeField] private float movementSpeed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if(transform.position.x > leftEdge)
            {
                transform.position = new Vector3 (transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Damge
            collision.GetComponent<Health>().TakeDamage(damage);

            // Disabling player movement for X time
            PlayerMovement movement = collision.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                StartCoroutine(DisableMovementTemporarily(movement));
            }
        }
    }

    private IEnumerator DisableMovementTemporarily(PlayerMovement movement)
    {
        movement.enabled = false;
        yield return new WaitForSeconds(disableTime);
        Health health = movement.GetComponent<Health>();
        if (health != null && health.currentHealth > 0)
        movement.enabled = true;
    }
}
