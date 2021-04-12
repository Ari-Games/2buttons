using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    
    public Transform Target;
    public SensorController Sensor;
    [HideInInspector] public EnemyMovement Movement;
    public Transform HealthBar;

    private Animator anim;
    public float Health = 10;
    private float MaxHealth;
    private bool isAttack = false;
    public float TimeAttack = 0.5f;

    private void Awake()
    {
        Movement = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
        MaxHealth = Health;
    }
    private IEnumerator Start()
    {
        yield return null;
        anim.SetBool("Run", true);
        //Movement = GetComponent<EnemyMovement>();
        //Movement.To(Target.position);
    }

    private void AllAnimationsOff()
    {
        anim.SetBool("Run", false);
    }

    private void SetAnim(string animation)
    {
        AllAnimationsOff();
        anim.SetBool(animation, true);
    }

    public void TakeDamage(float damage)
    {
        if (Health < damage)
        {
            Health = 0f;
        }
        else
        {
            Health -= damage;
            var scale = HealthBar.localScale;
            //var delta = damage/MaxHealth;
            //var deltaZ = scale.z - delta;
            scale.z = Health/MaxHealth;
            HealthBar.localScale = scale;
            anim.SetTrigger("Damage");
        }
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
        Hero.points += 5;
    }
    private void Update()
    {
        if (Sensor.DetectHero && !isAttack)
        {
            isAttack = true;
            StartCoroutine(Attack());
        }
        if (Health == 0)
        {
            anim.SetTrigger("Dead");
            StartCoroutine(Dead());
        }
        if (anim.GetBool("Run") == false)
            anim.SetBool("Run", true);
    }

    private IEnumerator Attack()
    {
        while (Health > 0)
        {
            yield return new WaitForSeconds(TimeAttack);
            anim.SetTrigger("Attack");
            Target.GetComponent<Hero>().TakeHealth(1);
            //Debug.Log("Detect !!!");
        }
    }
}
