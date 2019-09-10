using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUIController : MonoBehaviour
{
    public float m_CanvasToggleLockTime = 0.5f;

    private bool m_CanvasToggleLocked = false;
    private Canvas m_Canvas;
    // Start is called before the first frame update
    void Start()
    {
        m_Canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount == 3 || Input.GetMouseButtonDown(1)) && !m_CanvasToggleLocked)
        {
            m_Canvas.enabled = !m_Canvas.enabled;
            StartCoroutine(CanvasToggleLock());
        }
    }

    private IEnumerator CanvasToggleLock()
    {
        m_CanvasToggleLocked = true;
        yield return new WaitForSeconds(m_CanvasToggleLockTime);
        m_CanvasToggleLocked = false;
    }

    public void DirectionDropdownHandler(int index)
    {
        switch (index)
        {
            case 0:
                AppData.Instance.m_Direction = SpinDirection.CCW;
                break;
            case 1:
                AppData.Instance.m_Direction = SpinDirection.CW;
                break;
            default:
                break;
        }
    }

    public void SectorDropdownHandler(int index)
    {
        AppData.Instance.m_SelectedPrize = (PreselectedPrize)index;
    }
}
