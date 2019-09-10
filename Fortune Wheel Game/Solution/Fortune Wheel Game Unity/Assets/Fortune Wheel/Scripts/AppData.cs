using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppData : MonoBehaviour
{
    [Header("Feel free to modify the settings below")]
    public SpinDirection m_Direction = SpinDirection.CCW;
    public PreselectedPrize m_SelectedPrize = PreselectedPrize.Random;

    public static AppData Instance
    {
        private set; get;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}
