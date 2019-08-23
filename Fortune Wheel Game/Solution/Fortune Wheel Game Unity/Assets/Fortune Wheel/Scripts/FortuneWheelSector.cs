using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class FortuneWheelSector : MonoBehaviour
{
    [SerializeField] private int m_PrizeValue = -1;
    [SerializeField] private Text m_PrizeText;      // Visible in the Inspector, but not in the code

    public int PrizeValue { get { return m_PrizeValue; } }

    /// <summary>
    /// This method assumes that the pivot point of 
    /// the Fortune Wheel Sector is at the bottom center of the image 
    /// and that all Sectors are parented the same object
    /// and aligned to the center of the parent
    /// </summary>
    public void SetUpSector(int prizeValue = -1, Sprite sprite = null, float rotationZ = -1)
    {
        // This method is added in case the Fortune Wheel Sector is instantiated by code
        if (prizeValue >= 0)
        {
            m_PrizeValue = prizeValue;
            m_PrizeText.text = Utils.GetSpaceSeparatedNumberText(m_PrizeValue);
        }

        if (sprite != null)
            GetComponent<Image>().overrideSprite = sprite;

        if (rotationZ >= 0)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y, rotationZ);
    }
}
