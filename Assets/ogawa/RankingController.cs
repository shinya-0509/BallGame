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

    //Eclipseのサーブレットに接続するservletConnectorクラス
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
        Debug.Log("1【RankingController】GetRankingData()実行開始");
        //サーバー(サーブレット)に接続しランキングデータを取得
        // yield returnを使って、servletConnector.ServletGet()の処理が完了するまで待機
        yield return StartCoroutine(servletConnector.ServletGet());
        //ランキングデータを取得できた場合のみ、ランキングパネルを更新
        if (!servletConnector.IsError)
        {
            UpdateRankingTexts();
        }
    }

    void UpdateRankingTexts()
    {
        Debug.Log("3【RankingController】UpdateRankingTexts()実行開始");
        //servletConnectorがデータベースから取得したリストを取得
        List<Record> recordList = servletConnector.Ranking.recordList;
        //各ランクのパネルの 子オブジェクトのTextMeshProUGUIコンポーネントを取得
        TextMeshProUGUI[] tmps = GetComponentsInChildren<TextMeshProUGUI>();
        this.bestPointTMP.text = servletConnector.Ranking.recordList[0].score.ToString();

    }

    public IEnumerator InsertNewRecord_and_GetRankingData()
    {
        Debug.Log("5【RankingController】InsertNewRecord_and_GetRankingData()実行開始");
        // yield returnを使って、servletConnector.ServletPost()の処理が完了するまで待機
        yield return StartCoroutine(servletConnector.ServletPost());
        //新記録を無事登録できた場合のみ、新しいランキングデータを取得する
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


