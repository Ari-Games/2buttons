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


    private IEnumerator Start()
    {
        yield return null;
        MaxHealth = Health;
        Movement = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
        Movement.To(Target.position);
        SetAnim("Run");
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
            Health = 0;
        }
        else
        {
            Health -= damage;
            var scale = HealthBar.localScale;
            var delta = damage/MaxHealth;
            var deltaZ = scale.z - delta;
            scale.z = deltaZ;
            HealthBar.localScale = scale;
            anim.SetTrigger("Damage");
        }
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
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
    }

    private IEnumerator Attack()
    {
        while (Health > 0)
        {
            yield return new WaitForSeconds(TimeAttack);
            anim.SetTrigger("Attack");
            Sensor.hero.TakeHealth(3);
        }
    }
}
