using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    public enum SpinDirection { CW = -1, CCW = 1}
    public enum PreselectedPrize { Random = 0, _0 = 1, _50 = 2, _100 = 3, _200 = 4, _300 = 5, _500 = 6, _750 = 7, _1000 = 8}

    [Header("Do NOT modify the settings below")]
    public RectTransform m_RaycastRefPoint;
    public LayerMask m_RaycastLayer;
    public Animator m_Animator;
    [Header("Feel free to modify the settings below")]
    public SpinDirection m_Direction;
    public PreselectedPrize m_SelectedPrize;

    // Events stuff
    public delegate void StartSpinningDelegate();
    public delegate void StopSpinningDelegate(int number);
    public event StartSpinningDelegate OnStartSpinning;
    public event StopSpinningDelegate OnStopSpinning;


    // Singleton
    public static SpinController Instance { private set; get; }
    // Singleton
    private void Awake()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(this); // Don't Really want to destroy the whole gameobject
            return;
        }
        Instance = this;
    }

    public void Spin()
    {
        OnStartSpinning(); // Disable button, clear the winning sum from the previous spin

        int animNum = 0;
        if (m_SelectedPrize == PreselectedPrize.Random)
             animNum = Random.Range(1, 8) * (int) m_Direction;
        else
             animNum = (int) m_SelectedPrize * (int) m_Direction;
        
        m_Animator.SetInteger("animNum", animNum);
        StartCoroutine(ResetAnimNumberValue());
    }
       
    private void EvaluateWinningPrize()
    {
        // I am using 3D Raycast with Box Collider
        // Because I had issues with the 2D Raycast
        // The Spin button is used as a reference point, because it is 
        // in the center and below the spinning wheel
        RaycastHit hit;
        if (Physics.Raycast(m_RaycastRefPoint.position, Vector3.up, out hit, Mathf.Infinity, m_RaycastLayer))
        {
            int prizeValue = hit.collider.GetComponent<FortuneWheelSector>().PrizeValue;
            OnStopSpinning(prizeValue);
        }
        else // Display wrong number to indicate error
            OnStopSpinning(999999);
    }

    private IEnumerator ResetAnimNumberValue()
    {
        // After the animation has started, 
        // set the animNum variable to neutral position to avoid looping
        yield return null;
        m_Animator.SetInteger("animNum", 0);
        // wait for the duration of the animation to evaluate the result
        yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorStateInfo(0).length);
        EvaluateWinningPrize();
    }
}
