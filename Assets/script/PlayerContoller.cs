using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーンの遷移に必要

public class PlayerContoller : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;

    //検証用
    //Rigidbody2D obj;

    // NOTE 値はUnityエディタ側から指定できるようにした方が管理しやすい
    [SerializeField] float jumpForce;
    [SerializeField] float walkForce;
    [SerializeField] float maxWalkForce;

    //しきい値
    [SerializeField] float threshold;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        int key = 0;

        // 移動処理
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // iOS

            // 左右移動
            // スマホの場合は、左右ボタンが無いので加速度センサの仕組みを使う
            if (Input.acceleration.x > threshold) key = 1;
            if (Input.acceleration.x < -threshold) key = -1;


            // ジャンプする
            // 連打ジャンプできないようにする（上向き方向の速度が０ = 静止している時）
            if (Input.GetMouseButtonDown(0) &&
                (this.rigid2D.velocity.y == 0))
            {
                // NOTE Rigidbody2Dコンポーネントを使ってる場合は、
                // AddForceで力を加えて移動させる（transformでの移動だとcoliderでの当たり判定が保証されなくなる）
                // ransform.upは、y方向に長さ１のベクトルになる(0, 1, 0);
                this.rigid2D.AddForce(transform.up * jumpForce);


                // Unityエディタ上の、AnimatorタブにてWalk->Jumpへのクリップ遷移に指定されているトリガー名をONにして、クリップ遷移
                this.animator.SetTrigger("JumpTrigger");

            }
        }
        else
        {

            // 左右移動
            if (Input.GetKey(KeyCode.RightArrow)) key = 1;
            if (Input.GetKey(KeyCode.LeftArrow)) key = -1;


            // ジャンプする
            // 連打ジャンプできないようにする（上向き方向の速度が０ = 静止している時）
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // NOTE Rigidbody2Dコンポーネントを使ってる場合は、
                // AddForceで力を加えて移動させる（transformでの移動だとcoliderでの当たり判定が保証されなくなる）
                // ransform.upは、y方向に長さ１のベクトルになる(0, 1, 0);
                this.rigid2D.AddForce(transform.up * jumpForce);

            }

        }

        // プレイヤーの速度
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // スピード制限
        if (speedx < this.maxWalkForce)
        {
            //Debug.Log(speedx);
            // NOTE AddForceメソッドで、力をかけるとどんどん加速する
            // NOTE 力をかけるのを止めると、どんどん減速する
            this.rigid2D.AddForce(transform.right * key * walkForce);
        }


        // 動く方向に応じてプレイヤーの向きを反転
        if(key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // プレイヤーの速度に応じてアニメーションの速度を変える
        // アニメーションの再生速度を変えるには、Animatorコンポーネントの持つspeed変数を変更する
        this.animator.speed = speedx / 3.0f;


        // 画面外に出た場合最初のシーンを読み直して再スタートさせる
        if (transform.position.y < -10)
        {
            Debug.Log("再スタート");
            SceneManager.LoadScene("SampleScene");
        }
    }

    // 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1.colliderコンポーネントが設定されている＆isTrigger項目がチェックされているオブジェクトと接触した際に接触したオブジェクトの情報が来る。
        // 2.オブジェクト同士が、すり抜ける
        // NOTE PlayerContollerがアタッチされているオブジェクトもcolliderコンポーネントが前提

        Debug.Log(collision);

        // TODO 本来画面遷移は、Sceneを管理するクラスで行うべきなので将来敵に直す
        SceneManager.LoadScene("ClearScene");
    }
}
