using UnityEngine;
using System.Collections;

public class MysteryBox : MonoBehaviour
{
    public Transform[] PowerUps;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && PowerUps != null && PowerUps.Length > 0)
        {
            Debug.Log("setting player");
            Transform tmpPowerUp = PowerUps[Random.Range(0, PowerUps.Length)];
            // if(tmpPowerUp.gameObject.GetComponent<Boost>()) {
                tmpPowerUp.gameObject.GetComponent<Boost>().SetPlayer(other.gameObject);
            // }
            other.gameObject.GetComponent<ItemSlot>().SetItem(tmpPowerUp);
            Destroy(gameObject);
        }
    }

 
}