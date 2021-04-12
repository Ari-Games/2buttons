using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BossController : MonoBehaviour
{
    public Transform TargetPosition;

    public Hero hero;
    private NavMeshAgent agent;
    private Animator anim;
    public float Speed = 2;
    public float Health = 100;
    public float MaxHealth = 100;
    public float SpeedAttack = 2f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        anim.SetBool("Run", true);
    }

    private bool IsReachedDestination()
    {
        return Vector3.Distance(transform.position, TargetPosition.position) < 2f;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage < Health ? damage : 0;
        anim.SetTrigger("Damage");
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(SpeedAttack);
        hero.TakeHealth(5);
        anim.SetBool("Attack", true);
    }

    private void Update()
    {
        if (Health <= 0)
        {
            StartCoroutine(Dead());
        }   
        
        if (!IsReachedDestination())
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition.position, Speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Run", false);
            anim.SetBool("Idle", true);
            StartCoroutine(Attack());
        }
    }


    private IEnumerator Dead()
    {
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
