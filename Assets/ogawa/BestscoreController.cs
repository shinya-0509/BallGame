using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BestscoreController : MonoBehaviour
{
    //1位のパネルを入れた配列
    public GameObject[] rankingColumns;
    //Eclipseのサーブレットに接続するservletConnectorクラス
    public ServletConnector servletConnector;
    public IEnumerator GetRankingData()
    {
        Debug.Log("1【BestscoreController】GetRankingData()実行開始");
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
        Debug.Log("3【BestscoreController】UpdateRankingTexts()実行開始");
        //servletConnectorがデータベースから取得したリストを取得
        List<Record> recordList = servletConnector.Ranking.recordList;
        //リストの要素数分 繰り返す
        for (int i = 0; i < recordList.Count; i++)
        {
            //各ランクのパネルの 子オブジェクトのTextMeshProUGUIコンポーネントを取得
            TextMeshProUGUI[] tmps = rankingColumns[i].GetComponentsInChildren<TextMeshProUGUI>();
            // tmps[0] ⇒ ScoreTMP
            //ScoreのTextの値を変える
            tmps[0].text = recordList[i].score.ToString();
        }
    }
    public IEnumerator InsertNewRecord_and_GetRankingData()
    {
        Debug.Log("5【BestscoreController】InsertNewRecord_and_GetRankingData()実行開始");
        // yield returnを使って、servletConnector.ServletPost()の処理が完了するまで待機
        yield return StartCoroutine(servletConnector.ServletPost());
        //新記録を無事登録できた場合のみ、新しいランキングデータを取得する
        if (!servletConnector.IsError)
        {
            StartCoroutine(GetRankingData());
        }
    }
}