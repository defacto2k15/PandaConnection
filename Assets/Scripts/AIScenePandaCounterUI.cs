using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScenePandaCounterUI : MonoBehaviour
{
    public TMPro.TMP_Text numberOfPandas;
    public TMPro.TMP_Text numberOfPandasInForest;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.notificationManager.OnPandaAdded += OnPandaAdded;
        GameManager.instance.notificationManager.OnPandaReleased += OnPandaReleased;
        OnPandaAdded();
        OnPandaReleased();
    }

    private void OnPandaAdded()
    {
        numberOfPandas.SetText(GameManager.instance.pandaManager.pandasOnDisplay.Count.ToString());
    }

    private void OnPandaReleased()
    {
        numberOfPandasInForest.SetText(GameManager.instance.pandaManager.pandasReleasedToForrest.ToString());
    }
}
