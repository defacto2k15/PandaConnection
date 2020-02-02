using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaStatisticsUI : MonoBehaviour
{
    public bool choosingTwoPandas;
    public IPanda firstPanda;
    public IPanda secondPanda;

    public PandaRepresentation firstPandaRepresentation;
    public PandaRepresentation secondPandaRepresentation;
    public PandaRepresentation childPandaRepresentation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var panda = Util.RaycastPanda();
        if (panda == null)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (choosingTwoPandas)
            {
                if (firstPanda == panda)
                {
                    panda.Deselect(1);
                    firstPandaRepresentation.Clear();
                    firstPanda = null;
                    return;
                }
                if (secondPanda == panda)
                {
                    panda.Deselect(2);
                    secondPandaRepresentation.Clear();
                    secondPanda = null;
                    return;
                }
                if (firstPanda == null)
                {
                    firstPanda = panda;
                    panda.Select(1);
                    firstPandaRepresentation.LoadPanda(firstPanda);
                    return;
                }
                if (secondPanda == null)
                {
                    secondPanda = panda;
                    panda.Select(2);
                    secondPandaRepresentation.LoadPanda(secondPanda);
                    return;
                }
            }
            else
            {
                if (firstPanda == panda)
                {
                    panda.Deselect(1);
                    firstPandaRepresentation.Clear();
                    firstPanda = null;
                    return;
                }

                if (firstPanda != null)
                {
                    firstPanda.Deselect(1);
                }

                firstPanda = panda;
                panda.Select(1);
                firstPandaRepresentation.LoadPanda(firstPanda);
            }
        }
        
    }

    public void Show()
    {
        choosingTwoPandas = false;
        this.GetComponent<Animator>().Play("Show");
    }

    public void Hide()
    {
        choosingTwoPandas = true;
        this.GetComponent<Animator>().Play("Hide");
    }
}
