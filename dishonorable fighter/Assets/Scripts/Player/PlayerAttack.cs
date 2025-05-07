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

        // Отключаем хитбокс при запуске игры
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

    // Вызывается в момент начала удара
    public void EnableHitbox()
    {
        if (swordHitbox != null)
            swordHitbox.SetActive(true);
        if (attackClip != null)
            SoundManager.instance.PlaySound(attackClip);
    }

    // Вызывается в конце удара
    public void DisableHitbox()
    {
        if (swordHitbox != null)
            swordHitbox.SetActive(false);
    }
}
