using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class chapter1_talk : MonoBehaviour
{
    public GameObject talkUI;
    public TextAsset chapterdialog;//對話的資料
    public SpriteRenderer right,left,middle,middleRight,middleLeft;//人物圖片
    public Image backGround;//背景圖片
    public GameObject memoryMask;//回憶濾鏡
    public TMP_Text nameText;//顯示的名稱
    public TMP_Text whereText;//顯示的地點
    public TMP_Text dialog;//顯示的對話
    public Button nextButton;//next按鈕
    public GameObject optionButton;//
    public Transform buttonGroup;//選項父節點(自動排序)
    public List<Sprite> sprites = new List<Sprite>();//人物編號
    Dictionary<string, Sprite> characterImg = new Dictionary<string, Sprite>();//角色人物-->圖片
    public List<Sprite> backGroundSprites = new List<Sprite>();//人物編號
    Dictionary<string, Sprite> backGroundImg = new Dictionary<string, Sprite>();//角色人物-->圖片
    private int dialogcount = 0;//保存當前的對話Index;
    private string[]  dialogRows;
    

    private void Awake() 
    {
        characterImg["Angel"] = sprites[0];
        characterImg["Anna"] = sprites[1];
        characterImg["Calina"] = sprites[2];
        characterImg["Chole"] = sprites[3];
        characterImg["Ed"] = sprites[4];
        characterImg["Erin"] = sprites[5];
        characterImg["Eth"] = sprites[6];
        characterImg["Galore"] = sprites[7];
        characterImg["Jess"] = sprites[8];
        characterImg["Lily"] = sprites[9];
        characterImg["Paul"] = sprites[10];
        characterImg["Pierre"] = sprites[11];
        characterImg["Selina"] = sprites[12];
        characterImg["news"] = sprites[13];

        backGroundImg["AngelRoom"] = backGroundSprites[0];
        backGroundImg["AnnaRoom"] = backGroundSprites[1];
        backGroundImg["canteen"] = backGroundSprites[2];
        backGroundImg["CalinaRoom"] = backGroundSprites[3];
        backGroundImg["cave"] = backGroundSprites[4];
        backGroundImg["caveinterior"] = backGroundSprites[5];
        backGroundImg["CholeRoom"] = backGroundSprites[6];
        backGroundImg["dormitory"] = backGroundSprites[7];
        backGroundImg["elfSquare"] = backGroundSprites[8];
        backGroundImg["elfTown"] = backGroundSprites[9];
        backGroundImg["EthRoom"] = backGroundSprites[10];
        backGroundImg["factory"] = backGroundSprites[11];
        backGroundImg["GaloreRoom"] = backGroundSprites[12];
        backGroundImg["Gate"] = backGroundSprites[13];
        backGroundImg["hallway"] = backGroundSprites[14];
        backGroundImg["laboraoty"] = backGroundSprites[15];
        backGroundImg["lectureTheater"] = backGroundSprites[16];
        backGroundImg["office"] = backGroundSprites[17];
        backGroundImg["orcSquare"] = backGroundSprites[18];
        backGroundImg["orcTribal"] = backGroundSprites[19];
        backGroundImg["SelinaLaboraoty"] = backGroundSprites[20];
        backGroundImg["square"] = backGroundSprites[21];
        backGroundImg["tavernBasement"] = backGroundSprites[22];
        backGroundImg["tavern"] = backGroundSprites[23];
    }

    void Start()
    {
        talkUI.SetActive(true);
        readText(chapterdialog);
        readline();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartDelay(float sec)
    {
        StartCoroutine(Delay(sec));
    }

    IEnumerator Delay(float second)
    {
        // 打印初始訊息
        Debug.Log("延遲開始");

        // 等待 1 秒
        yield return new WaitForSeconds(second);

        // 打印延遲後的訊息
        Debug.Log("1 秒後執行");
    }
    public void UpdateText(string _name, string _text)
    {
        nameText.text = _name;
        dialog.text = _text;
    }
    public void UpdateImage(string _name, int _pos)
    {
        if(_pos == 1)//左
        {
            middleRight.sprite = null;
            middleLeft.sprite = null;
            if(right.sprite == characterImg[_name])
            {
                right.sprite = null;
            }
            else if(middle.sprite == characterImg[_name])
            {
                middle.sprite = null;
            }
            left.sprite = characterImg[_name];
        }
        else if(_pos == 2)//中
        {
            middleRight.sprite = null;
            middleLeft.sprite = null;
            if(right.sprite == characterImg[_name])
            {
                right.sprite = null;
            }
            else if(left.sprite == characterImg[_name])
            {
                left.sprite = null;
            }
            middle.sprite = characterImg[_name];
        }
        else if(_pos == 3)//右
        {
            middleRight.sprite = null;
            middleLeft.sprite = null;
            if(left.sprite == characterImg[_name])
            {
                left.sprite = null;
            }
            else if(middle.sprite == characterImg[_name])
            {
                middle.sprite = null;
            }
            right.sprite = characterImg[_name];
        }
        else if(_pos == 4)//中右
        {
            left.sprite = null;
            middle.sprite = null;
            right.sprite = null;
            if(middleLeft.sprite == characterImg[_name])
            {
                middleLeft.sprite = null;
            }
            middleRight.sprite = characterImg[_name];
        }
        else if(_pos == 5)//中左
        {
            left.sprite = null;
            middle.sprite = null;
            right.sprite = null;
            if(middleRight.sprite == characterImg[_name])
            {
                middleRight.sprite = null;
            }
            middleLeft.sprite = characterImg[_name];
        }
    }
    public void UpdateBackGround(string _where, int _effect)
    {
        whereText.text = _where;
        backGround.sprite = backGroundImg[_where];
        if(_effect == 1)
        {
            memoryMask.SetActive(true);
        }
        else
        {
            memoryMask.SetActive(false);
        }
    }
    public void readText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        
    }
    public void readline()
    {
        foreach(var row in dialogRows)
        {
            string[] cells = row.Split(',');
            if(cells[0] == "#" && int.Parse(cells[1]) == dialogcount)//一般句(人物+對話+圖片)
            {
                UpdateBackGround(cells[6],int.Parse(cells[7]));
                UpdateText(cells[2], cells[3]);
                int posIndex;
                switch (cells[4])
                {
                    case "left":
                        posIndex = 1;
                        break;
                    case "middle":
                        posIndex = 2;
                        break;
                    case "right":
                        posIndex = 3;
                        break;
                    case "middleright":
                        posIndex = 4;
                        break;
                    default:
                        posIndex = 5; // 默认值
                        break;
                }
                UpdateImage(cells[2], posIndex);

                dialogcount = int.Parse(cells[5]);
                break;
            }
            else if(cells[0] == "&" && int.Parse(cells[1]) == dialogcount)//選項
            {
                UpdateBackGround(cells[6],int.Parse(cells[7]));
                talkUI.SetActive(false);
                GenrateOption(int.Parse(cells[1]));
                buttonGroup.gameObject.SetActive(true);
            }
            else if(cells[0] == "*" && int.Parse(cells[1]) == dialogcount)//無人句(名稱+對話)
            {
                UpdateBackGround(cells[6],int.Parse(cells[7]));
                UpdateText(cells[2], cells[3]);

                dialogcount = int.Parse(cells[5]);
                break;
            }
            else if(cells[0] == "$" && int.Parse(cells[1]) == dialogcount)//等待句
            {
                UpdateBackGround(cells[6],int.Parse(cells[7]));
                //StartDelay(int.Parse(cells[2]));
                dialogcount = int.Parse(cells[5]);
            }
            else if(cells[0] == "NEXT")//小節結束句
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if(cells[0] == "END")//章節結束句
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    public void onClickNext()
    {
        readline();
    }
    public void GenrateOption(int _index)
    {
        string[] cells = dialogRows[_index + 1].Split(',');
        if(cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);//生成
            button.GetComponentInChildren<TMP_Text>().text = cells[3];
            button.GetComponent<Button>().onClick.AddListener
            (   
                delegate 
                {
                    OnOptionClick(int.Parse(cells[5]));
                }
            );
            GenrateOption(_index + 1);
        }   
    }
    public void OnOptionClick(int _id)
    {
        dialogcount = _id;
        int count = buttonGroup.childCount;
        for(int i = 0;i < count;i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
        buttonGroup.gameObject.SetActive(false);
        talkUI.SetActive(true);
        readline();
    }
}
