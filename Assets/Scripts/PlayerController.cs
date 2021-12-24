using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
   public float speed = 3.0f;
    
    public int maxHealth = 200;
    public int maxMana = 50;
    public float timeInvincible = 2.0f;
    
    public int health { get { return currentHealth; }}
    int currentHealth;

    public int mana { get { return currentMana; } }
    int currentMana;

    bool isInvincible;
    float invincibleTimer;

    public HealthBar healthBar;
    public ManaBar manaBar;
    public Death_menu death_Menu;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    bool firstStart = true;


    Vector2 lookDirection = new Vector2(1, 0);

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        int lastSaved = Int32.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT COUNT(*) From SavedGames"));
        currentHealth = Int32.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT HP From SavedGames Where ID = " + lastSaved));
        currentMana = Int32.Parse(MyDataBase.ExecuteQueryWithAnswer("SELECT Mana From SavedGames Where ID = " + lastSaved));

    


        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        manaBar.SetMana(currentMana);
        manaBar.SetMaxMana(maxMana);
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeMana(-5);
        }    


        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("MoveX", lookDirection.x);
        animator.SetFloat("MoveY", lookDirection.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontal*speed + vertical*speed));

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }


        if(currentHealth <= 0)
        {
            death_Menu.SetActive(true);
        }

    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }


    public void ChangeMana(int amount)
    {
        if (amount < 0)
        {

        }

        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        manaBar.SetMana(currentMana);
        Debug.Log(currentMana + "/" + maxMana);
    }
}
