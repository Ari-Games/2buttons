using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EventManager : MonoBehaviour
{
    public delegate void NewCombination(string combo); 
    public static event NewCombination newCombination;

    public delegate void StartGame();
    public static event StartGame OnBattleStart;

    [SerializeField]
    GameObject GameOverText;

    private void Start()
    {
        SquadController.OnCurrentButtleEnd += CurrentBattleEnd;
        Hero.OnGameOver += GameOver;
    }
    void GameOver()
    {
        GameOverText.SetActive(true);
    }
    IEnumerator GameRestart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
    string CreateCombination()
    {
        var len = Random.Range(1, 5);
        StringBuilder combo = new StringBuilder(len);
        char[] letters = new char[] { 'A', 'D' };
        for (int i = 0; i < len; i++)
        {
            combo.Append(letters[Random.Range(0, 2)]);
        }
        return combo.ToString();
    }
    private void Update()
    {
        if (Hero.points >= 5)
        {
            Hero.points -= 5;
            newCombination(CreateCombination());
        }
    }
    public void GameStart()
    {
        OnBattleStart();
        
    }
    public void CurrentBattleEnd()
    {
        OnBattleStart();
    }
    public void ApplicationExit()
    {
        PlayerPrefs.DeleteKey("Scene");
        Application.Quit();
    }
}
