using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Image))]
    public class GameOverScreen : MonoBehaviour
    {
        private Image _image;

        void Awake()
        {
            _image = this.GetComponentNotNull<Image>();
        }
        public void MakeVisible()
        {
            _image.enabled = true;
        }
    }
}
