using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float health;
    internal float currentHealth;
    private Animator animator;

    public Transform bar;
    public Transform healthBar;
    float barHidingTime;
    float startBarHidingTime;

    internal bool dazed;
    internal float dazedTime;
    public float startDazedTime;
    internal bool dead;

    internal int hitCounter;
    internal bool hit;

    public float defence;

    public int price;

    public EnemyHealthBar enemyHealth;
    public bool bossScript;

    public bool necromancer; 

    void Start()
    {
        startBarHidingTime = 2f;
        animator = GetComponent<Animator>();
        currentHealth = health;
        if (bossScript)
        {
            if (necromancer)
            {
                enemyHealth = GameObject.FindGameObjectWithTag("enemyHealth").GetComponent<EnemyHealthBar>();
            }

            enemyHealth.SetMaxHealth(health);
        }   
    }

    void Update()
    {
        if (bossScript)
        {
            enemyHealth.SetHealth(currentHealth);
        }
        else
        {
            SetSize();
            ShowHealthBar();
        }
    }

    public void GetDamage(int damage)
    {
        if (damage <= defence)
        {
            currentHealth -= 1;
        }
        else
        {
            currentHealth -= (damage - defence);
        }
       
        animator.SetBool("getHitBool", true);
        animator.SetTrigger("getHit");
        barHidingTime = startBarHidingTime;
        dazed = true;
        dazedTime = startDazedTime;
        hit = true;
        if (hitCounter < 3)
        {
            hitCounter++;
        }

        if (currentHealth <= 0)
        {
            Destroyed();
        }
    }

    public void GetHitAnimation()
    {
        animator.SetBool("getHitBool", false);
        hit = false;
    }    

    public void Destroyed()
    {
        dead = true;
        PlayerMovement.killCounter += 1;
        PlayerMovement.stoneKillCounter += 1;
        GoldScore.gold += price;
        PlayerPrefs.SetInt("Gold", GoldScore.gold);
    }

    void ShowHealthBar()
    {
        if (barHidingTime > 0 && currentHealth > 0)
        {
            healthBar.gameObject.SetActive(true);
            barHidingTime -= Time.deltaTime;
        }
        else
        {
            healthBar.gameObject.SetActive(false);
        }
    }

    void SetSize()
    {
        float size;
        size = currentHealth / health;
        bar.localScale = new Vector3(size, 1f);
    }
}
