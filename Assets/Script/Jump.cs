using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    public void JumpToChapterChoose()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(1);
    }
    public void JumpToPoint(int _scene)
    {
        SceneManager.LoadScene(_scene);
    }
    public void JumpToStart(string _scene)
    {
        SceneManager.LoadScene(0);
    }
}
