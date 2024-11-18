using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ServletConnector : MonoBehaviour
{
    //接続状況を表示するTMPro
    //public TextMeshProUGUI connectionInfoTMP;
    [SerializeField] GameDirector gameDirector;

    //動的WebアプリケーションのURL
    const string ROOTPATH = "http://localhost:8080/teamb_online/";

    //サーブレットからのレスポンスの文字列が入る変数
    string responseText;

    //接続エラーが起こったかどうかのフラグ
    bool isError;

    //JSON文字列からC#オブジェクトに変換したポイントランキングが入る
    Ranking ranking;

    //プロパティ
    public bool IsError
    {
        get { return isError; }
    }
    public Ranking Ranking
    {
        get { return ranking; }
    }

    //Getメソッド(ランキングデータの取得)
    public IEnumerator ServletGet()
    {
        Debug.Log("2-1【ServletConnector】ServletGet()実行開始");

        //変数の初期化
        responseText = "";
        isError = false;
        ranking = null;

        //UnityWebRequestクラスはWebサーバーとの通信に使用される

        //このクラスのオブジェクトは抱える情報量が多いので、
        //使用済になったら自動的に破棄されるようにusingステートメントを使う
        //UnityWebRequest.Getメソッド(引数は 通信先のURI)で、
        //サーバー(サーブレット)へ送信する内容を設定する
        using (UnityWebRequest request = UnityWebRequest.Get(ROOTPATH + "GetRankingServlet"))
        {
            //request.SendWebRequest()でサーブレットにリクエスト送信した後、
            //レスポンスが返ってくるまで待機
            yield return request.SendWebRequest();

            //サーブレットにアクセス成功したらrequest.resultがSuccessに変わる。
            //失敗したらConnectionErrorに変わる

            //アクセス成功の場合、サーブレット内の処理の成否でさらに分岐する
            if (request.result == UnityWebRequest.Result.Success)
            {
                //サーブレットから返ってきた文字列を取得
                responseText = request.downloadHandler.text;

                //文字列がnullでなかった場合(つまりサーブレット内の処理が成功した場合)
                if (responseText != "null")
                {
                    isError = false;
                    Debug.Log("2-2【ServletConnector】ServletGet() : 取得したJSON形式の文字列: " + responseText);

                    //サーブレットから返ってきた文字列(JSON)をC#のオブジェクト(Ranking型)に変換
                    ranking = JsonUtility.FromJson<Ranking>(responseText);
                }
                else//サーブレット内の処理が失敗した場合
                {
                    isError = true;

                    Debug.Log("2-2【ServletConnector】ServletGet() : ランキングデータの取得に失敗しました");

                    //エラーが発生したことをゲーム画面に表示
                    //connectionInfoTMP.text = "Database Connection Error";
                }
            }
            else //そもそもサーブレットにアクセスできなかった場合
            {
                isError = true;

                //request.responseCode⇒エラーの種類が番号で表される
                //0の場合⇒サーバーが起動していない、もしくはサーバーのアドレスが間違っている
                //404の場合⇒サーバーに接続できているが、
                //動的Webプロジェクト名もしくはサーブレット名が間違っている
                //request.error⇒エラーの説明文が入っている
                Debug.Log("2-2【ServletConnector】ServletGet() : responseCode = "
                + request.responseCode + " / " + request.error);

                //エラーが発生したことをゲーム画面に表示
                //connectionInfoTMP.text = "Server Connection Error";
            }
        }
    }
    //Postメソッド(新記録の送信)
    public IEnumerator ServletPost()
    {
        Debug.Log("6-1【ServletConnector】ServletPost()実行開始");

        //変数の初期化
        responseText = "";
        isError = false;
        ranking = null;

        //Webサーバーに送信するフォームデータを生成するクラス
        WWWForm form = new WWWForm();
        Debug.Log("score" + this.gameDirector.Point);
        //送りたいパラメータをフォームデータに入れる
        form.AddField("score", this.gameDirector.Point);
        //UnityWebRequestクラスはWebサーバーとの通信に使用される
        //このクラスのオブジェクトは抱える情報量が多いので、
        //使用済になったら自動的に破棄されるようにusingステートメントを使う
        //UnityWebRequest.Postメソッド(引数は 通信先のURI と 送信するフォームデータ)で、
        //サーバー(サーブレット)へ送信する内容を設定する
        using (UnityWebRequest request = UnityWebRequest.Post(ROOTPATH + "InsertRecordServlet", form))
        {

            //SendWebRequest()で実行しデータ送信開始
            //サーバー(サーブレット)からレスポンスが返ってくるまで時間がかかるので
            //yield returnでレスポンスが返ってくるまで次のコード実行を待つ
            yield return request.SendWebRequest();

            //サーブレットにアクセス成功したらrequest.resultがSuccessに変わる。
            //失敗したらConnectionErrorに変わる
            //サーブレットにアクセスできた場合、サーブレット内の処理の成否を表示
            if (request.result == UnityWebRequest.Result.Success)
            {
                //サーブレットから返ってきた文字列を取得
                responseText = request.downloadHandler.text;
                //サーブレット内の処理が成功した場合
                if (responseText == "SUCCESS")
                {
                    isError = false;

                    Debug.Log("6-2【ServletConnector】ServletPost(): レコード書き込み完了");

                    //データベースに登録したことをゲーム画面に表示
                    //connectionInfoTMP.text += "has been registered";
                }
                else //サーブレット内の処理が失敗した場合
                {
                    isError = true;

                    Debug.Log("6-2【ServletConnector】ServletPost(): レコード書き込みエラー");

                    //エラーが発生したことをゲーム画面に表示
                    //connectionInfoTMP.text += "Database Connection Error";
                }
            }
            else //そもそもサーブレットにアクセスできなかった場合
            {
                isError = true;

                //request.responseCode⇒エラーの種類が番号で表される
                //0の場合⇒サーバーが起動していない、もしくはサーバーのアドレスが間違っている
                //404の場合⇒サーバーに接続できているが、
                //動的Webプロジェクト名もしくはサーブレット名が間違っている
                //request.error⇒エラーの説明文が入っている
                Debug.Log("6-2【ServletConnector】ServletPost() : responseCode = "
                + request.responseCode + " / " + request.error);

                //エラーが発生したことをゲーム画面に表示
                //connectionInfoTMP.text += "Server Connection Error";
            }
        }
    }
}