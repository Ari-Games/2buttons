using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Target;
    [HideInInspector] public EnemyMovement Movement;

    private IEnumerator Start()
    {
        yield return null;
        Movement = GetComponent<EnemyMovement>();
        Debug.Log(Target.position);
        Movement.To(Target.position);
    }
}
