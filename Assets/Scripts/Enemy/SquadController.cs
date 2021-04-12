using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    public delegate void EndCurrentButtle();
    public static event EndCurrentButtle OnCurrentButtleEnd;
    [HideInInspector]
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
        //Go(Target);
        EventManager.OnBattleStart += Go;
    }

    private void LateUpdate()
    {
        if (Units.Count == 0)
        {
            OnCurrentButtleEnd();
            Destroy(gameObject);
        }
            
        if (Target != null)
            Destination();
    }
    private void Update()
    {
        
    }
    public void Go()
    {
        //StartCoroutine(StepFrame());
        //Target = target;
        last = sample[index].transform;
        last.eulerAngles = new Vector3(0, -90, 0);
        last.position = Target.position;
        Destination();
    }
    IEnumerator StepFrame()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        last = sample[index].transform;
        last.eulerAngles = new Vector3(0, -90, 0);
        last.position = Target.position;
        Destination();
    }
    private void UpdateUnits()
    {
        var nUnits = new List<EnemyController>();
        foreach (var unit in Units)
            if (unit != null)
                nUnits.Add(unit);
        Units = nUnits;
    }

    private void Destination()
    {
        //UpdateUnits();
        UpdateUnits();
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i] != null && sample[index].Points != null)
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
    public void OnDestroy()
    {
        EventManager.OnBattleStart -= Go;
    }
}
