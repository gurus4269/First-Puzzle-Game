using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOrNot : MonoBehaviour
{
    public TMP_Text number1,number2,number3,number4;
    private int num1,num2,num3,num4;
    // Start is called before the first frame update
    void Start()
    {
        num1 = int.Parse(number1.text);
        num2 = int.Parse(number2.text);
        num3 = int.Parse(number3.text);
        num4 = int.Parse(number4.text);
    }
    public bool CheckEnd()
    {
        num1 = int.Parse(number1.text);
        num2 = int.Parse(number2.text);
        num3 = int.Parse(number3.text);
        num4 = int.Parse(number4.text);
        if(num1 == 0 && num2 == 7 && num3 == 1 && num4 == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
