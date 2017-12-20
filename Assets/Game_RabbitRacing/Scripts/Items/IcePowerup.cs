using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePowerup : MonoBehaviour, Item
{

    public Transform m_hazardSpawnpoint;
    public GameObject m_iceHazard;
    private BaseKartMovement m_baseKartMovement;
    //Spawns the IceHazard to the rear of the Kart.
    public void Use(GameObject user)
    {
        Instantiate(m_iceHazard, m_hazardSpawnpoint.transform.position, Quaternion.identity);
    }
}
