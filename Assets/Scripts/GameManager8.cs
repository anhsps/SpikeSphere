using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using System.Threading.Tasks;

public class GameManager8 : MonoBehaviour
{
    public static GameManager8 instance { get; private set; }

    [HideInInspector] public int score;

    [SerializeField] private TextMeshProUGUI scoreText, scoreText2, bestText;
    [SerializeField] private GameObject loseMenu, pauseMenu;
    [SerializeField] private RectTransform losePanel, pausePanel;
    [SerializeField] private float topPosY = 250f, middlePosY, tweenDuration = 0.3f;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    async void Start()
    {
        Time.timeScale = 1f;
        UpdateScore(0);

        await HidePanel(loseMenu, losePanel);
        await HidePanel(pauseMenu, pausePanel);
    }

    public void Retry() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void PauseGame() => ShowPanel(pauseMenu, pausePanel);

    public async void ResumeGame()
    {
        await HidePanel(pauseMenu, pausePanel);
        Time.timeScale = 1f;
    }

    public void GameLose() => EndGame(loseMenu, losePanel, 3);

    private void EndGame(GameObject menu, RectTransform panel, int soundIndex)
    {
        SoundManager8.instance.PlaySound(soundIndex);
        ShowPanel(menu, panel);
    }

    private void ShowPanel(GameObject menu, RectTransform panel)
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        menu.GetComponent<CanvasGroup>().DOFade(1, tweenDuration).SetUpdate(true);
        panel.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);
    }

    private async Task HidePanel(GameObject menu, RectTransform panel)
    {
        menu.GetComponent<CanvasGroup>().DOFade(0, tweenDuration).SetUpdate(true);
        await panel.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        menu.SetActive(false);
    }

    // score
    public void UpdateScore(int score)
    {
        this.score = score;
        scoreText.text = score + "m";
        scoreText2.text = score + "m";
        UpdateBest();
    }

    private void UpdateBest()
    {
        if (score > LoadBest())
            PlayerPrefs.SetInt("best", score);
        bestText.text = LoadBest() + "m";
    }

    private int LoadBest() => PlayerPrefs.GetInt("best", 0);
}
