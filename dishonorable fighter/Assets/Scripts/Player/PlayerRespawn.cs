using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UI_Manager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindFirstObjectByType<UI_Manager>();
    }

    public void CheckRespawn()
    {
        //Check if checkoints is availible
        if(currentCheckpoint == null)
        {
            //Show game over screen
            uiManager.GameOver();
            return;
        }

        playerHealth.Respawn(); // Restore player health and reset animation
        transform.position = currentCheckpoint.position; // Move player to checkpoint location

        // Move the camera to the checkpoint's room
        Transform currentRoom = currentCheckpoint.parent;
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentRoom);

        // Activate the room manually
        Room room = currentRoom.GetComponent<Room>();
        if (room != null)
            room.ActivateRoom(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}