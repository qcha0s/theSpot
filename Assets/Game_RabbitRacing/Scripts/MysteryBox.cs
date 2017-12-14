using UnityEngine;
using System.Collections;

public class MysteryBox : MonoBehaviour
{
    public Transform[] PowerUps;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"/* && PowerUps != null && PowerUps.Length > 0*/)
        {
            //other.gameObject.GetComponent<ItemSlot>().SetItem(PowerUps[Random.Range(0, PowerUps.Length)]);
            Destroy(gameObject);
        }
    }

 
}