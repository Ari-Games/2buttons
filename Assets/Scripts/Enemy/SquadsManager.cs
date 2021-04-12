using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadsManager : MonoBehaviour
{
   
    public Transform SpawnPosition;
    [HideInInspector]
    public List<SquadController> Squads;

    private SquadController currentSquad;

    private void Start()
    {
        Squads = new List<SquadController>();
        foreach (Transform squad in transform)
        {
            Squads.Add(squad.GetComponent<SquadController>());
            squad.gameObject.SetActive(false);
        }
    }

    private void UpdateSquads()
    {
        var nSquads = new List<SquadController>();
        foreach (SquadController squad in Squads)
            if (squad != null && squad.Units.Count > 0)
                nSquads.Add(squad);
        Squads = nSquads;
    }
    private void Update()
    {
        if (Squads.Count > 0 && (currentSquad == null || currentSquad.Units.Count == 0))
        {
            currentSquad = Squads[Squads.Count-1];
            currentSquad.gameObject.SetActive(true);
        }
        UpdateSquads();
    }
}
