using Assets.Scripts.PandaLogic.Genetics;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Sounds;
using UnityEngine;

public class PandaStatisticsUI : MonoBehaviour
{
    public bool choosingTwoPandas;
    public IPanda firstPanda;
    public IPanda secondPanda;

    public PandaRepresentation firstPandaRepresentation;
    public PandaRepresentation secondPandaRepresentation;
    public PandaRepresentation childPandaRepresentation;

    private ChildPandaCreator _childPandaCreator;

    private ChildPandaCreator childPandaCreator
    {
        get
        {
            if (_childPandaCreator == null)
            {
                _childPandaCreator = GameObject.FindObjectOfType<ChildPandaCreator>(); 
            }
            return _childPandaCreator;
        }
    }

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
        SoundManager.instance.PlayOneShotSound(SoundType.MenuClick);
            if (choosingTwoPandas)
            {
                if (firstPanda == panda)
                {
                    panda.Deselect(1);
                    firstPandaRepresentation.Clear();
                    firstPanda = null;
                    childPandaRepresentation.Clear();
                    return;
                }
                if (secondPanda == panda)
                {
                    panda.Deselect(2);
                    secondPandaRepresentation.Clear();
                    secondPanda = null;
                    childPandaRepresentation.Clear();
                    return;
                }
                if (firstPanda == null)
                {
                    firstPanda = panda;
                    panda.Select(1);
                    firstPandaRepresentation.LoadPanda(firstPanda.GetStats());
                    if(firstPanda!=null && secondPanda != null && firstPanda.GetGender() != secondPanda.GetGender())
                    {
                        PredictThirdPanda(firstPanda, secondPanda);
                    }
                    return;
                }
                if (secondPanda == null)
                {
                    secondPanda = panda;
                    panda.Select(2);
                    secondPandaRepresentation.LoadPanda(secondPanda.GetStats());
                    if (firstPanda != null && secondPanda != null && firstPanda.GetGender()!=secondPanda.GetGender())
                    {
                        PredictThirdPanda(firstPanda, secondPanda);
                    }
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
                firstPandaRepresentation.LoadPanda(firstPanda.GetStats());
            }
        }
        
    }

    private void PredictThirdPanda(IPanda firstPanda, IPanda secondPanda)
    {
        var pandaStats = childPandaCreator.CreateChildStats(firstPanda.GetStats(), secondPanda.GetStats());
        childPandaRepresentation.LoadPanda(pandaStats);
    }


    public void Show()
    {
        choosingTwoPandas = false;
        this.GetComponent<Animator>().Play("Show");
    }

    public void Hide()
    {
        choosingTwoPandas = true;
        childPandaRepresentation.Clear();
        this.GetComponent<Animator>().Play("Hide");
    }
}
