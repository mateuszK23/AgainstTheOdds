using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private int currentValue;
    private int maxValue;

    [SerializeField]
    private Image fill;
    [SerializeField]
    private Text amountText;

    public void initValues(int currentVal, int maxVal ){
        this.currentValue = currentVal;
        this.maxValue = maxVal;
        amountText.text = currentValue.ToString();
        calculateFillAmount();
    }
       
    public void setValue(int currentVal)
    {
        if(currentValue!= 0)
        {
            if (currentVal < 0) currentVal = 0;
            this.currentValue = currentVal;
            amountText.text = currentValue.ToString();
            calculateFillAmount();
        }
    }

    private void calculateFillAmount()
    {
            float fillAmount = (float)currentValue / (float)maxValue;
            fill.fillAmount = fillAmount;
    }
}
