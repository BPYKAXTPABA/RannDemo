using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject swordHitbox;
    [SerializeField] private float attackCooldown;
    [SerializeField] private AudioClip attackClip;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        // Disable hitbox when starting the game
        if (swordHitbox != null)
            swordHitbox.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown)
        {
            if (playerMovement.isJumping())
            {
                anim.SetTrigger("jumpAttack");
            }
            else
            {
                anim.SetTrigger("attack");
            }
            cooldownTimer = 0;
        }

        cooldownTimer += Time.deltaTime;
    }

    // Called at the moment the hit begins
    public void EnableHitbox()
    {
        if (swordHitbox != null)
            swordHitbox.SetActive(true);
        if (attackClip != null)
            SoundManager.instance.PlaySound(attackClip);
    }

    // Called at the moment the hit ends
    public void DisableHitbox()
    {
        if (swordHitbox != null)
            swordHitbox.SetActive(false);
    }
}
