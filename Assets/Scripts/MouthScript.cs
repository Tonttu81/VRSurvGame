using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthScript : MonoBehaviour
{
    PlayerUI playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food" || other.tag == "Drink") // Jos consumable osuu suu-triggeriin, kutsuu consume-methodin consumablesta ja tallentaa arvot
        {
            Vector3 stats = other.GetComponent<ConsumableScript>().Consume();
            playerStats.hunger += stats.y;
            playerStats.thirst += stats.z;

            Destroy(other.gameObject);
        }
    }
}
