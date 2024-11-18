using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RankingController : MonoBehaviour
{

    //public GameObject RankingCanvas;
    public GameObject[] rankingColumns;
    private string[] ranking;
    // [SerializeField] RankingController rc;
    [SerializeField] TextMeshProUGUI bestPointTMP;

    //Eclipse�̃T�[�u���b�g�ɐڑ�����servletConnector�N���X
    public ServletConnector servletConnector;
    GameObject rankingText;

    public string[] GetRanking { get => ranking; }

    /*
    public void ShowRanking()
    {
        RankingCanvas.SetActive(true);
    }
    */


    public IEnumerator GetRankingData()
    {
        Debug.Log("1�yRankingController�zGetRankingData()���s�J�n");
        //�T�[�o�[(�T�[�u���b�g)�ɐڑ��������L���O�f�[�^���擾
        // yield return���g���āAservletConnector.ServletGet()�̏�������������܂őҋ@
        yield return StartCoroutine(servletConnector.ServletGet());
        //�����L���O�f�[�^���擾�ł����ꍇ�̂݁A�����L���O�p�l�����X�V
        if (!servletConnector.IsError)
        {
            UpdateRankingTexts();
        }
    }

    void UpdateRankingTexts()
    {
        Debug.Log("3�yRankingController�zUpdateRankingTexts()���s�J�n");
        //servletConnector���f�[�^�x�[�X����擾�������X�g���擾
        List<Record> recordList = servletConnector.Ranking.recordList;
        //�e�����N�̃p�l���� �q�I�u�W�F�N�g��TextMeshProUGUI�R���|�[�l���g���擾
        TextMeshProUGUI[] tmps = GetComponentsInChildren<TextMeshProUGUI>();
        this.bestPointTMP.text = servletConnector.Ranking.recordList[0].score.ToString();

    }

    public IEnumerator InsertNewRecord_and_GetRankingData()
    {
        Debug.Log("5�yRankingController�zInsertNewRecord_and_GetRankingData()���s�J�n");
        // yield return���g���āAservletConnector.ServletPost()�̏�������������܂őҋ@
        yield return StartCoroutine(servletConnector.ServletPost());
        //�V�L�^�𖳎��o�^�ł����ꍇ�̂݁A�V���������L���O�f�[�^���擾����
        if (!servletConnector.IsError)
        {
            StartCoroutine(GetRankingData());
        }
    }

    private void Start()
    {
        this.rankingText = GameObject.Find("ranking");
    }
}


