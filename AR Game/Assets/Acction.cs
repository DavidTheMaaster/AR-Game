using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acction : MonoBehaviour
{
    public int life = 5;
    public int ammonition = 0;
    public GameObject opponent;
    public GameObject gun, ammo, shield;
    public Animator gun_animation;
    public bool shoot, protect, reload;
    private bool action = true;

    private bool gun_seen, ammo_seen, shield_seen;
    private Acction opponent_action;
    private float counter = 5;

    public Text actionLabel = null;
    public Text actionLabelShade = null;

    void Start()
    {
        shoot = false;
        protect = false;
        reload = false;
        counter = GameObject.Find("Canvas").GetComponent<CountDownTimer>().currentTime;
        gun_animation = gun.transform.GetChild(0).GetComponent<Animator>();
        opponent_action = opponent.GetComponent<Acction>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w"))
        {
            Reset();
        }

        if (counter < 2.0f && counter > 1.0f)
        {
            if (!gun_seen && !ammo_seen && shield_seen)
            {
                protect = true;
            }

            if (!shield_seen && !ammo_seen && gun_seen)
            {
                shoot = true;
            }

            if (!gun_seen && !shield_seen && ammo_seen)
            {
                reload = true;
            }
            TestSeen();
        }

        if (counter < 1.0f)
        {
            if (action)
            {
                if (protect)
                {
                    Protect();
                }
                if (shoot)
                {
                    Shoot();
                }
                if (reload)
                {
                    Recharge();
                }
            }
        }

        if (counter < -1)
        {
            actionLabel.gameObject.SetActive(false);
            actionLabelShade.gameObject.SetActive(false);
        }

        Timer();
    }

    void Shoot()
    {
        Debug.Log("SHOOT");

        actionLabel.gameObject.SetActive(true);
        actionLabel.text = "PUM!";
        actionLabelShade.gameObject.SetActive(true);
        actionLabelShade.text = "PUM!";

        if (gun_animation != null && ammonition > 0)
        {
            ammonition--;
            gun_animation.SetBool("Shoot", true);
            if (!opponent_action.protect)
            {
                opponent_action.life--;
                action = false;
            }
        }
    }

    void Recharge()
    {
        action = false;
        ammonition += 1;
        Debug.Log("RECHARGE");

        actionLabel.gameObject.SetActive(true);
        actionLabel.text = "RELOAD!";
        actionLabelShade.gameObject.SetActive(true);
        actionLabelShade.text = "RELOAD!";
    }

    void Protect()
    {
        Debug.Log("PROTECT");

        actionLabel.gameObject.SetActive(true);
        actionLabel.text = "PROTECT!";
        actionLabelShade.gameObject.SetActive(true);
        actionLabelShade.text = "PROTECT!";
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
        counter = GameObject.Find("Canvas").GetComponent<CountDownTimer>().currentTime;
    }

    void Reset()
    {
        shoot = false;
        protect = false;
        reload = false;
        GameObject.Find("Canvas").GetComponent<CountDownTimer>().currentTime = 5.0f;
        action = true;
    }
}
