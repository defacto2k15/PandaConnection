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

    private void Awake()
    {
        startGameButton.onClick.AddListener(StartGame);
        middleGameButton.onClick.AddListener(StartMiddle);
        tutorialButton.onClick.AddListener(StartTutorial);
    }

    private void StartGame()
    {
        StartCoroutine(UnloadScene());
    }

    private IEnumerator UnloadScene()
    {
        SceneManager.LoadScene(startGameUIScene, LoadSceneMode.Additive);
        SceneManager.LoadScene(startGameAIScene, LoadSceneMode.Additive);
        yield return null;
        SceneManager.UnloadSceneAsync("MenuScene");
    }


    private void StartMiddle()
    {
        SceneManager.UnloadSceneAsync("MenuScene");
        SceneManager.LoadScene(middleGameAIScene, LoadSceneMode.Additive);
        SceneManager.LoadScene(middleGameUIScene, LoadSceneMode.Additive);
    }

    private void StartTutorial()
    {
        tutorialObject.SetActive(true);
    }
}
