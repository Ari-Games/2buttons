using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.Text;
using System;

public class InputController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

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

    void NewCombination()
    {
        var len = random.Next(1,4);
        StringBuilder combo = new StringBuilder(len);
        char[] letters = new char[] { 'A', 'D' };
        for (int i = 0; i < len; i++)
        {
            combo.Append(letters[random.Next(0, 1)]);
        }
        combinations.Add(new Combination(combo.Length,combo.ToString()));

    }
    void Update()
    {
        ProcessInput();
    }
    void ProcessInput()
    {
        char? keyCode = null;
        if (Input.GetKeyDown(KeyCode.A))
        {
            //animator.SetTrigger("A");
            keyCode = 'A';
            inputBuffer.AddBuffer('A');
            Debug.Log('A');
        } else
        if (Input.GetKeyDown(KeyCode.D))
        {
            //animator.SetTrigger("D");
            keyCode = 'D';
            inputBuffer.AddBuffer('D');
            Debug.Log('D');
        }
        if (Time.time - inputBuffer.LastTime>1f)
        {
            inputBuffer.Clear();
            //FirstLevelHit(keyCode);
        }
        Combination currenCombination = inputBuffer.CheckBuffer(combinations);
        if (currenCombination!= null)
        {
            Debug.Log("Found combination - " + currenCombination.ComboName);
            HitByCombination(currenCombination,keyCode);
            return;
        }       
       
    }

    private void HitByCombination(Combination combination,char? keyCode)
    {
        if (combination.ComboName.Length == 1)
        {
            FirstLevelHit(keyCode);
            return;
        }
        animator.SetTrigger(combination.ComboName.Length + "_" + random.Next(1, 3));
        OnAttack(combination.Damage);
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
