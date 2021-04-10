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

    List<Assets.Scripts.Combination> combinations;
    InputBuffer inputBuffer;
    System.Random random = new System.Random();
    void Start()
    {
        combinations = new List<Combination>();
        AddCombination();
        inputBuffer = new InputBuffer(5);
    }

    public void AddCombination(string comboName = "AA",int damage = 5)
    {
        combinations.Add(new Combination(damage,comboName));
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
        }
        Combination currenCombination = inputBuffer.CheckBuffer(combinations);
        if (currenCombination!= null)
        {
            Debug.Log("Found combination - " + currenCombination.ComboName);
            HitByCombination(currenCombination);
            return;
        }
        FirstLevelHit(keyCode);
       
    }

    private void HitByCombination(Combination combination)
    {
        animator.SetTrigger(combination.ComboName.Length + "_" + random.Next(1, 3));
    }

    void FirstLevelHit(char? keyCode)
    {
        if (keyCode!=null)
        {
            animator.SetTrigger(keyCode.ToString());
        }
    }
}
