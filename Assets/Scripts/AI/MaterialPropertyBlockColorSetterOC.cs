using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RadarBattleground
{
    [ ExecuteInEditMode ]
    public class MaterialPropertyBlockColorSetterOC : MonoBehaviour
    {
        public Color Color;
        public bool ChangeInChildren=false;
        private MaterialPropertyBlock _propBlock;
        private Renderer _renderer;

        void Awake()
        {
            AffirmFieldArePresent();
        }

        void Update()
        {
            UpdateColor();
        }

        public void ChangeColorAndUpdate(Color newColor)
        {
            Color = newColor;
            UpdateColor();
        }

        public void UpdateColor()
        {
            AffirmFieldArePresent();
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_Color", Color);
            _renderer.SetPropertyBlock(_propBlock);
            if (ChangeInChildren)
            {
                GetComponentsInChildren<MaterialPropertyBlockColorSetterOC>().ToList().ForEach(c =>
                {
                    if (c != this)
                    {
                        c.Color = Color;
                        c.UpdateColor();
                    }
                });
            }
        }

        private void AffirmFieldArePresent()
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<Renderer>();
            }
            if (_propBlock == null)
            {
                _propBlock = new MaterialPropertyBlock();
            }
        }
    }
}
