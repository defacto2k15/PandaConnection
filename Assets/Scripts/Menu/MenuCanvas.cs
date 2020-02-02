using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Sounds;
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

    void Start()
    {
        SoundManager.instance.PlayBackgroundTheme(SoundType.OpeningTheme);
    }

    private void Exit()
    {
        SoundManager.instance.PlayOneShotSound(SoundType.MenuAcceptBig);
        Application.Quit();
    }

    private void StartGame()
    {
        SoundManager.instance.PlayOneShotSound(SoundType.MenuAcceptBig);
        StartCoroutine(UnloadScene());
    }

    private void StartGameMiddle()
    {
        SoundManager.instance.PlayOneShotSound(SoundType.MenuAcceptBig);
        StartCoroutine(StartMiddle());
    }

    private IEnumerator UnloadScene()
    {
        SoundManager.instance.PlayBackgroundTheme(SoundType.BackgroundTheme);
        SceneManager.LoadScene(startGameUIScene, LoadSceneMode.Additive);
        SceneManager.LoadScene(startGameAIScene, LoadSceneMode.Additive);
        yield return null;
        SceneManager.UnloadSceneAsync("MenuScene");
    }

    private IEnumerator StartMiddle()
    {
        SoundManager.instance.PlayBackgroundTheme(SoundType.BackgroundTheme);
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