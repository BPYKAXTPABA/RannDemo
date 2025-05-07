using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] arrowTraps; // arrow traps array

    private Vector3[] initialPosotion;

    private void Awake()
    {
        // Save the initial position for enemies
        initialPosotion = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                initialPosotion[i] = enemies[i].transform.position;
        }
    }

    public void ActivateRoom(bool _status)
    {
        //Activate or Deactivate enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosotion[i];
            }
        }

        //Activate or Reset arrow traps
        for (int i = 0; i < arrowTraps.Length; i++)
        {
            if (arrowTraps[i] != null)
            {
                arrowTraps[i].SetActive(_status);

                // Reset arrow trap (reset internal arrows to firePoint)
                ArrowTrap trap = arrowTraps[i].GetComponent<ArrowTrap>();
                if (trap != null)
                    trap.ResetTrap();
            }
        }
    }
}
