using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    //public Canvas opening,ending;//開場動畫、結束動畫
    public List<Canvas> canvas = new List<Canvas>();//動畫編號
    Dictionary<string, Canvas> canvasName = new Dictionary<string, Canvas>();//劇情-->動畫
    private int[] collect;
    // Start is called before the first frame update
    void Start()
    {
        int count = canvas.Count;
        collect = new int[count-3];
        foreach(var canvas in canvasName.Values)
        {
            canvas.gameObject.SetActive(false);
        }
        CanvasStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake() 
    {
        canvasName["opening"] = canvas[0];
        canvasName["ending"] = canvas[1];
        canvasName["normal"] = canvas[2];
        canvasName["HPTN"] = canvas[3];
    }

    //控制開場
    public void CanvasStart()
    {
        canvasName["opening"].gameObject.SetActive(true);
        canvasName["normal"].gameObject.SetActive(false);
    }

    public void OpenClose()
    {
        canvasName["opening"].gameObject.SetActive(false);
        canvasName["normal"].gameObject.SetActive(true);
    }
    //控制場景物件
    public void ObjectStart(int _canvasID)
    {
        //Debug.Log("AAAAA");
        canvas[_canvasID].gameObject.SetActive(true);
        canvasName["normal"].gameObject.SetActive(false);
    }
    public void ObjectClosed(int _canvasID)
    {
        canvas[_canvasID].gameObject.SetActive(false);
        canvasName["normal"].gameObject.SetActive(true);
        collect[_canvasID-3] = 1;
        EndStart();
    }

    private void EndStart()
    {
        if(CheckEndOrNot() == true)
        {
            canvas[2].gameObject.SetActive(false);
            canvas[1].gameObject.SetActive(true);
        }
        
    }
    public void EndClosed()
    {
        canvas[1].gameObject.SetActive(false);
    }

    private bool CheckEndOrNot()
    {
        foreach(int check in collect)
        {
            if(check != 1)
            {
                return false;
            }
        }
        return true;
    }
}
