using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Sounds;
using UnityEngine;
using UnityEngine.UI;

public class ReleaseToForrestButton : MonoBehaviour
{
    public IPanda currentPanda;
    public Image progress;
    public Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(ReleaseAPandaToForrest);
        myButton.interactable = false;
        progress.fillAmount = 0;
        GameManager.instance.notificationManager.OnPandaSelected += AllowRelease;
        GameManager.instance.notificationManager.OnPandaDeSelected += DisallowRelease;
    }

    private void ReleaseAPandaToForrest()
    {
        if ((currentPanda as MonoBehaviour).isActiveAndEnabled)
        {

                SoundManager.instance.PlayOneShotSound( SoundType.Yay);
            currentPanda.GoToForest();
            GameManager.instance.pandaManager.pandasReleasedToForrest++;
            progress.fillAmount = (float)GameManager.instance.pandaManager.pandasReleasedToForrest / (float)GameManager.instance.pandaManager.pandasToWin;
            myButton.interactable = false;
            currentPanda = null;
        }
    }

    private void AllowRelease(IPanda panda)
    {
        currentPanda = panda;
        if (GameManager.instance.pandaManager.pandasOnDisplay.Count > 2)
        {
            myButton.interactable = true;
        }
    }

    private void DisallowRelease(IPanda panda)
    {
        if (currentPanda == panda)
        {
            currentPanda = null;
            myButton.interactable = false;
        }
    }
}
