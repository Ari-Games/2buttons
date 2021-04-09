using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.Text;
public class InputController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    List<Assets.Scripts.Combination> combinations;
    InputBuffer inputBuffer;
    void Start()
    {
        combinations = new List<Combination>();
        AddCombination();
        inputBuffer = new InputBuffer(5);
    }

    void AddCombination(string comboName = "AA")
    {
        combinations.Add(new Combination(5,comboName));
    }
    void Update()
    {
        ProcessInput();
    }
    void ProcessInput()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            //animator.SetTrigger("A");
            inputBuffer.AddBuffer('A');
            Debug.Log('A');
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //animator.SetTrigger("D");
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
        }
    }
}
