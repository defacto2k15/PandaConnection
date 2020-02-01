﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DEBUG {
    [RequireComponent(typeof(Button))]
    public class DEBUGAddMoney : MonoBehaviour
    {
        public int amount;
        private void Awake()
        {
            this.GetComponent<Button>().onClick.AddListener(Click);
        }

        private void Click()
        {
            GameManager.instance.MoneyManager.AddMoney(amount);
        }
    }
}
