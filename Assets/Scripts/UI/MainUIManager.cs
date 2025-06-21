using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private TextMeshProUGUI nameTemp;

    private void Start()
    {
        nameTemp = GameObject.Find("NameTemp").GetComponent<TextMeshProUGUI>();
        nameTemp.text = "Your Name is; " + InternalManager.Instance.playerName;
    }

    public void BackToMenu()
    {
        InternalManager.Instance.SaveGameJson();
        SceneManager.LoadScene(0);
    }
}
