using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCrowdManager : MonoBehaviour
{
    public float giveMoneyTimeInterval = 5.0f;
    public float minMoneyToGive = 1.0f;
    public float maxMoneyToGive = 200.0f;
    public float wantedPandasNumber = 50.0f;
    public float wantedMutationsNumber = 100.0f;
    public float cashMultiplayer = 100;

    private void Awake()
    {
        StartCoroutine(GiveMoney());
    }

    private IEnumerator GiveMoney()
    {
        while (true)
        {
            GameManager.instance.MoneyManager.AddMoney(CalculateAppreciation());
            yield return new WaitForSeconds(giveMoneyTimeInterval);
        }
    }

    private int CalculateAppreciation()
    {
        //zwiedzajacy chcieli by widziec 100 lub wiecej pand
        float pandasSeenCoefficient = (float)GameManager.instance.pandaManager.pandasOnDisplay.Count / wantedPandasNumber;

        float genesMutatedCoefficient = (float)GameManager.instance.pandaManager.timeGenesMutated / wantedMutationsNumber;

        float appreciationCoefficient = Mathf.Min(pandasSeenCoefficient + genesMutatedCoefficient, 1.0f);

        //Debug.Log($"AppreciationCoefficient: {appreciationCoefficient}");
        return (int)Mathf.Lerp(minMoneyToGive, maxMoneyToGive, appreciationCoefficient * cashMultiplayer / 100f);
    }
}