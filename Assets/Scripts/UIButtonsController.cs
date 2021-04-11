using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class UIButtonsController : MonoBehaviour
{
    [SerializeField]
    Image AButton;

    [SerializeField]
    Image DButton;

    [SerializeField]
    Color PressedColor;

    [SerializeField]
    GameObject NewCombinationText;

    

    Color simpleColor;

    void Start()
    {
        simpleColor = AButton.color;
        EventManager.newCombination += PostEnable;
    }
    void PostEnable(string comboName)
    {
        NewCombinationText.SetActive(true);
        StartCoroutine(DisableFromSeconds(NewCombinationText,4));
    }
    public static IEnumerator DisableFromSeconds(GameObject go,float seconds)
    {
        yield return new WaitForSeconds(seconds);
        go?.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            AButton.color = PressedColor;
        }
        else
        {
            AButton.color = simpleColor;
        }
            
        if (Input.GetKey(KeyCode.D))
        {
            DButton.color = PressedColor;
        }
        else
        {
            DButton.color = simpleColor;
        }
    }
}
