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

    [SerializeField]
    GameObject GameWinText;

    private void Start()
    {
        SquadController.OnCurrentButtleEnd += CurrentBattleEnd;
        Hero.OnGameOver += GameOver;
        BossController.OnGameWin += GameWin;
    }
    void GameOver()
    {
        GameOverText.SetActive(true);
        StartCoroutine(GameRestart());
    }
    IEnumerator GameRestart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }

    void GameWin()
    {
        GameWinText.SetActive(true);
        StartCoroutine(GameRestart());
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
        StartCoroutine(StepFrame());
        
    }
    IEnumerator StepFrame()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        OnBattleStart();
    }
    public void CurrentBattleEnd()
    {
        OnBattleStart();
    }
    public void ApplicationExit()
    {
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Scene");
    }
}
