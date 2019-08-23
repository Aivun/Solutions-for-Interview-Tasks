using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text m_WinBoxText;
    public Button m_SpinButton;
    
    public static UIController Instance
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

    // Start is called before the first frame update
    void Start()
    {
        m_SpinButton.onClick.AddListener(SpinButton_OnClick);
        SpinController.Instance.OnStartSpinning += StartSpinnigHandler;
        SpinController.Instance.OnStopSpinning += StopSpinnigHandler;
    }

    public void SpinButton_OnClick()
    {
        SpinController.Instance.Spin(); // <3 Singleton - less opportunities for "MissingReferenceException"
    }

    public void StartSpinnigHandler()
    {
        m_WinBoxText.text = "Spinning...";
        m_SpinButton.interactable = false;
    }

    public void StopSpinnigHandler(int number)
    {
        m_WinBoxText.text = Utils.GetSpaceSeparatedNumberText(number);
        m_SpinButton.interactable = true;
    }
}
