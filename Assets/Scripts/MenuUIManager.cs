using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static TMP_InputField enterName;
    private GameObject confirmPop;
    private TextMeshProUGUI nameViewer;
    public TextMeshProUGUI highScoreName;
    public TextMeshProUGUI highScore;

    private void Start()
    {
        highScoreName = GameObject.Find("HighScoreName").GetComponent<TextMeshProUGUI>();
        highScore = GameObject.Find("HighScore").GetComponent <TextMeshProUGUI>();
        enterName = GameObject.Find("Enter Name").GetComponent<TMP_InputField>();
        confirmPop = GameObject.Find("Confirm").gameObject;
        nameViewer = GameObject.Find("Name Viewer").GetComponent<TextMeshProUGUI>();
        confirmPop.SetActive(false);
        InternalManager.Instance.LoadGameJson();
        ShowHighScore();
    }

    public void NamePlayer()
    {
        if (string.IsNullOrEmpty(enterName.text))
        {
            InternalManager.Instance.playerName = "Unknown";
        }
        else
        {
            InternalManager.Instance.playerName = enterName.text;
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        InternalManager.Instance.SaveGameJson();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ClickStartButton()
    {
        NamePlayer();
        nameViewer.text = InternalManager.Instance.playerName;
        confirmPop.SetActive(true);
    }

    public void ClickCancelButton()
    {
        confirmPop.SetActive(false);
    }

    private void ShowHighScore()
    {
        List<int> scores = InternalManager.Instance.scoreList;
        List<string> names = InternalManager.Instance.nameList;
            highScore.text = "Score \n";
            highScoreName.text = "Name \n";
            foreach (int score in scores)
            {
                highScore.text += score + "\n";
            }
            foreach (string name in names)
            {
                highScoreName.text += name + "\n";
            }
    }

    public void ClickSaveButton()
    {
        InternalManager.Instance.SaveGameJson();
    }

    public void ClickLoadButton()
    {
        InternalManager.Instance.LoadGameJson();
        ShowHighScore();
    }

    public void ClickRemoveButton()
    {
        InternalManager.Instance.RemonveGameJson();
        ShowHighScore();
    }
}
