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

    Stack<int> attacks = new Stack<int>();

    public delegate void GameOver();
    public static event GameOver OnGameOver;
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
            OnGameOver();
            
        }
        health -= damage;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && attacks.Count !=0)
        {
            int damage = attacks.Pop();
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }
    RaycastHit hit;
    void Shoot()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100) && hit.collider.tag == "Enemy")
        {
            hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(10);
        }

    }
    void FullHP()
    {
        health = MaxHealth;
    }
    void Attack(int damage)
    {
        attacks.Push(damage);
    }
}
