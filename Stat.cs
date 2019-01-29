using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stat : MonoBehaviour
{
    private Image content;
    private float currentFill; 
    public float MaxValue { get; set; }

    public float MyCurrentValue
    {
        get
        {
            return CurrentValue;
        }

        set
        {
            CurrentValue = value;
            currentFill = CurrentValue/MaxValue;
        }
    }

    private float CurrentValue;
    
    // Start is called before the first frame update
    void Start()
    {
        content =  GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        content.fillAmount = currentFill;
    }
    public void Inicialize(float CurrentValue,float MaxValue)
    {
        this.MaxValue = MaxValue;
        MyCurrentValue = CurrentValue;
    }
}
