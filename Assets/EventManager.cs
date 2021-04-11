using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void NewCombination(); 
    public static event NewCombination newCombination;
    private void Update()
    {
        if (Hero.points >= 5)
        {
            Hero.points -= 5;
            newCombination();
        }
    }
}
