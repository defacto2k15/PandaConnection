using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AI
{
    public class BarOverPanda : MonoBehaviour
    {
        [SerializeField] private BarType _type;
        [SerializeField] private Color _color0;
        [SerializeField] private Color _color1;
        [SerializeField] private AnimationCurve _valueToColorCurve;
        [SerializeField] private DummyPanda _panda;

        [SerializeField] private Slider _slider;
        [SerializeField] private Image _image;

        void Update()
        {
            float factor;
            if (_type == BarType.Ero)
            {
                factor = _panda.GetEro() / GameManager.instance.pandaManager.GetMaximumEro();
            }
            else
            {
                factor = _panda.GetFullness() / GameManager.instance.pandaManager.GetMaximumFullness();
            }

            _image.color = _color0 + _valueToColorCurve.Evaluate(factor) * (_color1 - _color0);
            _slider.value = factor;
        }
    }

    public enum BarType
    {
        Ero , Fullness
    }
}
