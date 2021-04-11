using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void NewCombination(); 
    public static event NewCombination newCombination;

    public delegate void StartGame();
    public static event StartGame onGameStart;

    private void Update()
    {
        if (Hero.points >= 5)
        {
            Hero.points -= 5;
            newCombination();
        }
    }
}
