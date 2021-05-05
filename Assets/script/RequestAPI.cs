using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using ResponseDataSerialize;


public class RequestAPI : MonoBehaviour
{

    [SerializeField] public GameObject cloudPrefab;
    UnityWebRequest request;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutineを使って実行
        // ローカルサーバーからデータ取得して雲を生成
        StartCoroutine(createCloud());

    }

    // 雲を生成するメソッド
    // IEnumeratorはコルーチンを使うため方
    private IEnumerator createCloud()
    {
        // 読み込み時にAPI呼び出し
        // localhostでのみ有効
        this.request = UnityWebRequest.Get("http://localhost:3000/TOP");

        //// yield return を差し込めばコルーチン中断できる
        //Debug.Log("First.");
        //yield return null;

        //Debug.Log("Second.");
        //yield return null;


        //SendWebRequestを実行し、送受信開始
        yield return request.SendWebRequest();

        //3.isNetworkErrorとisHttpErrorでエラー判定
        //if (request.isHttpError || request.isNetworkError ||

        //isNetworkError非推奨の警告出るので以下に修正
        if (this.request.result == UnityWebRequest.Result.ConnectionError)
        {
            //4.エラー確認
            Debug.Log(this.request.error);
        }
        else
        {
            //4.結果確認
            //Debug.Log(request);
            //Debug.Log(request.downloadHandler);
            //Debug.Log(request.downloadHandler.text);
            var data = this.request.downloadHandler.text;

            // 返却されたjsonをクラス変換
            PositionDatas jsonDatas = JsonUtility.FromJson<PositionDatas>(data);
            Debug.Log(jsonDatas.data.Count);

            // idを基準に昇順
            jsonDatas.data.Sort((a, b) => a.id - b.id);

            // 生成
            for (int i = 0; i < jsonDatas.data.Count; i++)
            {
                //レスポンス情報から位置とスケールを設定
                GameObject cloud = Instantiate(cloudPrefab);
                cloud.transform.position = new Vector3(
                    jsonDatas.data[i].pos_x,
                    jsonDatas.data[i].pos_y,
                    jsonDatas.data[i].pos_z);

                cloud.transform.localScale = new Vector3(
                    jsonDatas.data[i].scale_x,
                    jsonDatas.data[i].scale_y,
                    jsonDatas.data[i].scale_z);
            }

        }
    }
}
