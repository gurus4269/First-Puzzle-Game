using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class number_controler : MonoBehaviour
{
    public Button Up,Down;
    public TMP_Text number;
    // Start is called before the first frame update
    void Start()
    {
        number.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtomUP()
    {
        int A = int.Parse(number.text);
        A += 1;
        if(A > 9)
        {
            A = 0;
        }
        number.text = A.ToString();
    }
    public void ButtomDOWN()
    {
        int A = int.Parse(number.text);
        A -= 1;
        if(A < 0)
        {
            A = 9;
        }
        number.text = A.ToString();
    }
}
