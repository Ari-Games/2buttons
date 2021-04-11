using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField]
    int MaxHealth;

    [SerializeField]
    Image HealthBar;

    int health;

    public static int points = 1;
    EnemyController targetEnemy = null;
    void Start()
    {
        health = MaxHealth;
        InputController.OnAttack += Attack;
    }

    void Update()
    {
        HealthBar.fillAmount = (float)health / MaxHealth;        
    }

    public void TakeHealth(int damage)
    {
        if (damage > health)
        {
            health = 0;
            
        }
        health -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            targetEnemy = other.gameObject.GetComponent<EnemyController>();
            Debug.Log("Entered");
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        targetEnemy = null;
    }
    void Attack(int damage)
    {
        targetEnemy?.TakeDamage(damage);
        Debug.Log("Attack");
    }
}
