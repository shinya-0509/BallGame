using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
//using UnityEditor.VersionControl;
using UnityEngine;
//using UnityEngine.Apple.ReplayKit;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    //public TextMeshProUGUI userNameTMP;
    //public TextMeshProUGUI userInputFieldTMP;
    //public TextMeshProUGUI inputInfoTMP;
    //public TextMeshProUGUI connectionInfoTMP;
    public int point;

    public ServletConnector servletConnector;
    public RankingController rankingController;
    public GameObject rankingPanel;
    //public GameObject inputPanel;
    //public GameObject startButton;
    //1位を記録を超えたかどうか比較するために3位のポイントを入れる
    public TextMeshProUGUI rank1stPointTMP;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] TextMeshProUGUI resultScore;
    [SerializeField]  RankingController rc;
    [SerializeField] Image nextBall;
    [SerializeField] GameGenerater ballGenerater;
    [SerializeField] bool isPlaying = true;
    [SerializeField] bool isDead = false;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] private DropperController dc;
    GameObject stockBall;
    Transform[] children;
    GameObject parent;
    private BallController ballController;
    public int inputTextLength;
    Queue<GameObject> queue = new Queue<GameObject>();
    List<GameObject> ballList = new List<GameObject>();
    private int postCount;
    //登録されたユーザーネーム
    //public string name;
    //userName用のインプットフィールドに入力された文字数


    string[] ranking;
    void Start()
    {
        Application.targetFrameRate = 60;
        this.point = 0;
        this.postCount = 0;
        this.stockBall = this.ballGenerater.Pop();
        this.queue.Enqueue(this.stockBall);
        parent = GameObject.Find("Balls");

        //各種設定を初期化
        InitializeSettings();
        //サーブレットとの接続状況は非表示でスタート
        //connectionInfoTMP.text = "";
        //ランキングデータを取得
        StartCoroutine(rankingController.GetRankingData());
    }

    void Update()
    {
        this.resultScore.text = this.point.ToString();
        this.DisplayPoint();
        if (this.queue.Count == 0)
        {
            this.stockBall = this.ballGenerater.Pop();
            this.queue.Enqueue(this.stockBall);
        }
        this.nextBall.sprite = this.queue.Peek().GetComponent<SpriteRenderer>().sprite;

        if (isDead)
        {
            int count = 0;
            children = new Transform[parent.transform.childCount];
            foreach (Transform Child in parent.transform)
            {
                children[count] = Child;
                Child.GetComponent<BallController>().CollisionOff();
                count++;
            }
            GameOver();
            this.ranking = rc.GetRanking;

            dc.enabled = false;
            //ゲームプレイ終了後は各種設定を初期化
            InitializeSettings();

            while(this.postCount < 1)
            {
                StartCoroutine(rankingController.InsertNewRecord_and_GetRankingData());
                this.postCount++;
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            this.isDead = true;
        }
    }//update

    public void InitializeSettings()
    {
        //InputPanelを表示する
        //inputPanel.SetActive(true);
        //StartButtonを表示する
        //startButton.SetActive(true);

        //バスケットのコライダーを無効にし、
        //タイムオーバー後に残っているボールにあたってもポイントが増えないようにする。
        //ballGenerater.GetComponent<BoxCollider>().enabled = false;
    }

    public void StartButtonClicked() //Start Button が押されたときに実行
    {
        isPlaying = true;
        //ポイントをリセット
        point = 0;
        //StartButtonを非表示にする
        //startButton.SetActive(false);
        //空文字にすることでconnectionInfoを非表示にする
        //connectionInfoTMP.text = "";
        //名前が登録されていなければ代わりに"No Name"を登録する
        /*
        if (name == "")
        {
            name = "No Name";
        }
        */
    }
    public void SubmitButtonClicked() //Submit Button が押されたときに実行
    {
        //入力された文字数で処理を変える
        //inputFieldの仕様により、実際よりも1文字多くカウントされるので注意
        /*
        if (inputTextLength <= 3) //2文字以下の場合
        {
            inputInfoTMP.text = "Too short name";
        }
        else if (10 <= inputTextLength) //9文字以上の場合
        {
            inputInfoTMP.text = "Too long name";
        }
        else //3~8文字の名前なら登録を受け付ける  
        {
            name = userInputFieldTMP.text; //入力された名前を登録
            inputInfoTMP.text = " Name has been registered";
        }
        */
    }
    /*--------------------------------------------------------------------------*/
    public void AddPoint(int i)
    {
        this.Point += i;
        Debug.Log(i);
        Debug.Log("Point" + Point);
    }
    private void DisplayPoint()
    {
        this.pointText.text = this.Point.ToString();
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        gameOverPanel.SetActive(true);
    }

    public List<GameObject> BallList { get => ballList; set => ballList = value; }
    public Queue<GameObject> Queue { get => queue; set => queue = value; }
    public int Point { get => point; set => point = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
}

