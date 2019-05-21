using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Acction : MonoBehaviour
{
    public int life = 5;
    public int ammonition = 0;
    public GameObject opponent;
    public GameObject gun, ammo, shield;
    public Animator gun_animation;
    public bool shoot, protect, reload;
    private bool action = true;
    private AudioSource shootSound, shieldSound, reloadSound;

    public Text life_counter, life_shadow_counter;
    public Text ammo_counter, ammo_shadow_counter;

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

        shootSound = gameObject.GetComponent<AudioSource>();
        shieldSound = gameObject.GetComponent<AudioSource>();
        reloadSound = gameObject.GetComponent<AudioSource>();
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
        }

        TestSeen();

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

        if (counter < -2)
            Reset();

        Timer();
    }

    void Shoot()
    {
        Debug.Log("SHOOT");

        if (gun_animation != null && ammonition > 0)
        {

            actionLabel.gameObject.SetActive(true);
            actionLabel.text = "PUM!";
            actionLabelShade.gameObject.SetActive(true);
            actionLabelShade.text = "PUM!";

            shootSound.Play();

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

        reloadSound.Play();
    }

    void Protect()
    {
        Debug.Log("PROTECT");

        actionLabel.gameObject.SetActive(true);
        actionLabel.text = "PROTECT!";
        actionLabelShade.gameObject.SetActive(true);
        actionLabelShade.text = "PROTECT!";

        shieldSound.Play();
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

        ammo_counter.text = ammonition.ToString();
        ammo_shadow_counter.text = ammonition.ToString();
        life_counter.text = life.ToString() + "/5";
        life_shadow_counter.text = life.ToString() + "/5";
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
        if(life == 0)
        {
            if (transform.gameObject.name == "Player Blue")
                SceneManager.LoadScene("RetryRedWins");
            if (transform.gameObject.name == "Player Red")
                SceneManager.LoadScene("RetryBlueWins");
        }
    }
}
