using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum LevelResult { LevelWin, BossWin, Lose }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    GameObject popUpGO;
    [SerializeField]
    Text PopupText;

    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void LevelEnd(LevelResult levelResult)
    {
        switch (levelResult)
        {
            case LevelResult.LevelWin:
                StartCoroutine(LoadNextLevel(3));
                break;

            case LevelResult.BossWin:
                StartCoroutine(BossWin(2));
                break;

            case LevelResult.Lose:
                PopupText.text = "DIE";
                popUpGO.SetActive(true);
                break;
        }
    }

    IEnumerator LoadNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync("Arena_2");
    }

    IEnumerator BossWin(float delay)
    {
        yield return new WaitForSeconds(delay);
        PopupText.text = "WIN";
        popUpGO.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("Arena_1");
    }
}
