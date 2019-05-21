using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acction : MonoBehaviour
{
    public GameObject gun, ammo, shield;
    public Animator gun_animation;
    private bool gun_seen, ammo_seen, shield_seen;
    private int counter = 0;

    void Start()
    {
        gun_animation = gun.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0)
        {
            if (!gun_seen && !ammo_seen && shield_seen)
            {
                Protect();
            }

            if (!shield_seen && !ammo_seen && gun_seen)
            {
                Shoot();
            }

            if (!gun_seen && !shield_seen && ammo_seen)
            {
                Recharge();
            }

            TestSeen();
        }

        Timer();
    }

    void Shoot()
    {
        Debug.Log("SHOOT");

        if(gun_animation != null)
        {
            gun_animation.SetBool("Shoot", true);
        }
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

        if(gun_seen && ammo_seen && shield_seen)
        {
            if (gun_animation != null)
            {
                gun_animation.SetBool("Shoot", false);
            }
        }
    }

    void Timer()
    {

    }
}
