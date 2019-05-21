using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acction : MonoBehaviour
{
    public GameObject gun, ammo, shield;
    private bool gun_seen, ammo_seen, shield_seen;

    // Update is called once per frame
    void Update()
    {
        if(!gun_seen && !ammo_seen)
        {
            Protect();
        }

        if (!shield_seen && !ammo_seen)
        {
            Shoot();
        }

        if (!gun_seen && !shield_seen)
        {
            Recharge();
        }

        TestSeen();
    }

    void Shoot()
    {
        Debug.Log("SHOOT");
    }

    void Recharge()
    {
        Debug.Log("RECHARGE");
    }

    void Protect()
    {
        Debug.Log("PROTECT");
    }

    void TestSeen()
    {
        gun_seen = gun.GetComponent<DefaultTrackableEventHandler>().seen;
        ammo_seen = ammo.GetComponent<DefaultTrackableEventHandler>().seen;
        shield_seen = shield.GetComponent<DefaultTrackableEventHandler>().seen;
    }
}
