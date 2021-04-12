using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.Text;
using System;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Text combinationsList;

    public delegate void Attack(int damage);
    public static event Attack OnAttack;

    List<Assets.Scripts.Combination> combinations;
    InputBuffer inputBuffer;
    System.Random random = new System.Random();
    void Start()
    {
        combinations = new List<Combination>();
        AddCombination("A");
        AddCombination("D");
        inputBuffer = new InputBuffer(5);
        EventManager.newCombination += NewCombination;
    }

    public void AddCombination(string comboName = "AA",int damage = 5)
    {
        combinations.Add(new Combination(damage,comboName));
    }

    void NewCombination(string comboName)
    {
        if (combinations.Count >= 2)
        {
            combinations.Clear();
        }
        
        combinations.Add(new Combination(comboName.Length,comboName));

    }
    void Update()
    {
        ProcessInput();
        UpdateCombinationsList();
    }

    private void UpdateCombinationsList()
    {
        combinationsList.text = "";
        foreach (var item in combinations)
        {
            combinationsList.text += item.ComboName + '\n';
        }
    }

    void ProcessInput()
    {
        char? keyCode = null;
        if (Input.GetKeyDown(KeyCode.A))
        {
            keyCode = 'A';
            inputBuffer.AddBuffer('A');
        } else
        if (Input.GetKeyDown(KeyCode.D))
        {
            keyCode = 'D';
            inputBuffer.AddBuffer('D');
        }
        if (Time.time - inputBuffer.LastTime>1f)
        {
            inputBuffer.Clear();
        }
        Combination currenCombination = inputBuffer.CheckBuffer(combinations);
        if (currenCombination!= null)
        {
            //Debug.Log("Found combination - " + currenCombination.ComboName);
            HitByCombination(currenCombination,keyCode);
            return;
        }       
       
    }

    static int fightLevel = 0;
    static string[] triggers = new string[] {"2_1", "2_2", "3_1", "3_2","4_1" };
    private void HitByCombination(Combination combination,char? keyCode)
    {
        if (combination.ComboName.Length == 1)
        {
            FirstLevelHit(keyCode);
            return;
        }
        int index = Mathf.Abs(combination.GetHashCode()) % triggers.Length;
        string trigger = triggers[index];
        animator.SetTrigger(trigger);
        OnAttack(combination.Damage*2);
    }

    void FirstLevelHit(char? keyCode)
    {
        if (keyCode !=null)
        {
            animator.SetTrigger(keyCode.ToString());
            OnAttack(5);
        }       
        
    }
}
