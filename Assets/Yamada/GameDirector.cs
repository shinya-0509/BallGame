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
    //1�ʂ��L�^�𒴂������ǂ�����r���邽�߂�3�ʂ̃|�C���g������
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
    //�o�^���ꂽ���[�U�[�l�[��
    //public string name;
    //userName�p�̃C���v�b�g�t�B�[���h�ɓ��͂��ꂽ������


    string[] ranking;
    void Start()
    {
        Application.targetFrameRate = 60;
        this.point = 0;
        this.postCount = 0;
        this.stockBall = this.ballGenerater.Pop();
        this.queue.Enqueue(this.stockBall);
        parent = GameObject.Find("Balls");

        //�e��ݒ��������
        InitializeSettings();
        //�T�[�u���b�g�Ƃ̐ڑ��󋵂͔�\���ŃX�^�[�g
        //connectionInfoTMP.text = "";
        //�����L���O�f�[�^���擾
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
            //�Q�[���v���C�I����͊e��ݒ��������
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
        //InputPanel��\������
        //inputPanel.SetActive(true);
        //StartButton��\������
        //startButton.SetActive(true);

        //�o�X�P�b�g�̃R���C�_�[�𖳌��ɂ��A
        //�^�C���I�[�o�[��Ɏc���Ă���{�[���ɂ������Ă��|�C���g�������Ȃ��悤�ɂ���B
        //ballGenerater.GetComponent<BoxCollider>().enabled = false;
    }

    public void StartButtonClicked() //Start Button �������ꂽ�Ƃ��Ɏ��s
    {
        isPlaying = true;
        //�|�C���g�����Z�b�g
        point = 0;
        //StartButton���\���ɂ���
        //startButton.SetActive(false);
        //�󕶎��ɂ��邱�Ƃ�connectionInfo���\���ɂ���
        //connectionInfoTMP.text = "";
        //���O���o�^����Ă��Ȃ���Α����"No Name"��o�^����
        /*
        if (name == "")
        {
            name = "No Name";
        }
        */
    }
    public void SubmitButtonClicked() //Submit Button �������ꂽ�Ƃ��Ɏ��s
    {
        //���͂��ꂽ�������ŏ�����ς���
        //inputField�̎d�l�ɂ��A���ۂ���1���������J�E���g�����̂Œ���
        /*
        if (inputTextLength <= 3) //2�����ȉ��̏ꍇ
        {
            inputInfoTMP.text = "Too short name";
        }
        else if (10 <= inputTextLength) //9�����ȏ�̏ꍇ
        {
            inputInfoTMP.text = "Too long name";
        }
        else //3~8�����̖��O�Ȃ�o�^���󂯕t����  
        {
            name = userInputFieldTMP.text; //���͂��ꂽ���O��o�^
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

