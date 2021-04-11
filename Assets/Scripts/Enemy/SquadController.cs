using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public List<EnemyController> Units;
    [SerializeField] private List<SampleComponent> sample;

    public LayerMask layerMask;
    private int index;
    private Transform last;

    public Transform Target;

    private void Start()
    {
        foreach(Transform unit in transform)
        {
            EnemyController enemy = unit.GetComponent<EnemyController>();
            Units.Add(enemy);
        }
        foreach (var item in sample)
            item.Set(false);
        last = transform;
        SetSample(0);
        Destination();
    }

    private void LateUpdate()
    {
       
        last = sample[index].transform;
        //last.forward = (Target.position - last.position).normalized;
        last.position = Target.position;
        Destination();
        
    }

    private void UpdateUnits()
    {
        List<EnemyController> nUnits = new List<EnemyController>();
        foreach (var unit in Units)
            if (unit != null)
                nUnits.Add(unit);
        Units = nUnits;
    }

    private void Destination()
    {
        UpdateUnits();
        for (int i = 0; i < Units.Count; i++)
        {
            Units[i].Movement.To(sample[index].Points[i].position);
        }
    }

    public void SetSample(int i)
    {
        sample[index].Set(false);
        if (i < 0)
            index = 0;
        else if (i > Units.Count - 1)
            index = Units.Count - 1;
        else
            index = i;
        sample[index].Set(true);
        sample[index].transform.forward = last.forward;
        sample[index].transform.position = last.position;
        Destination();
    }
}
