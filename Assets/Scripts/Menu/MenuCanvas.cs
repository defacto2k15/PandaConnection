using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    public SceneField menuScene;

    [Header("Start game scenes")]
    public SceneField startGameAIScene;

    public SceneField startGameUIScene;

    [Header("Middle game scenes")]
    public SceneField middleGameAIScene;

    public SceneField middleGameUIScene;

    public GameObject tutorialObject;

    public Button startGameButton;
    public Button middleGameButton;
    public Button tutorialButton;
    public Button exitButton;

    private void Awake()
    {
        startGameButton.onClick.AddListener(StartGame);
        middleGameButton.onClick.AddListener(StartGameMiddle);
        tutorialButton.onClick.AddListener(StartTutorial);
        exitButton.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        StartCoroutine(UnloadScene());
    }

    private void StartGameMiddle()
    {
        StartCoroutine(StartMiddle());
    }

    private IEnumerator UnloadScene()
    {
        SceneManager.LoadScene(startGameUIScene, LoadSceneMode.Additive);
        SceneManager.LoadScene(startGameAIScene, LoadSceneMode.Additive);
        yield return null;
        SceneManager.UnloadSceneAsync("MenuScene");
    }

    private IEnumerator StartMiddle()
    {
        SceneManager.LoadScene(middleGameAIScene, LoadSceneMode.Additive);
        SceneManager.LoadScene(middleGameUIScene, LoadSceneMode.Additive);
        yield return null;
        SceneManager.UnloadSceneAsync("MenuScene");
    }

    private void StartTutorial()
    {
        tutorialObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}