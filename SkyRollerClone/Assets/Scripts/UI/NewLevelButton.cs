﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone.UI
{
    public class NewLevelButton : MonoBehaviour
    {
        public void StartNewLevel()
        {
            if (GameManager.IsInitialized)
            {
                GameManager.Instance.StartNewLevel();
            } else
            {
                Debug.LogError("GameManager not found");
            }
        }
    }
}