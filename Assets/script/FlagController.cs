using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 検証用
public class FlagController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1.colliderコンポーネントが設定されている＆isTrigger項目がチェックされているオブジェクトと接触した際に接触したオブジェクトの情報が来る。
        // 2.オブジェクト同士が、すり抜ける
        // NOTE FlagControllerがアタッチされているオブジェクトもcolliderコンポーネントが前提

        Debug.Log("OnTriggerEnter2D");
        Debug.Log(collision);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // colliderコンポーネントが設定されている＆isTrigger項目がチェックされていないオブジェクトの情報がくる
    //    // isTriggerとの違いは衝突判定もされるためオブジェクト同士が、すり抜けない
    //    Debug.Log("OnCollisionEnter2D");
    //    Debug.Log(collision);
    //}
}
