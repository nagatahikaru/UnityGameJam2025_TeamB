//
//Playerの動きを制御するクラス
//
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Script : MonoBehaviour
{    
    public float moveSpeed = 0.0f;//プレイヤーの移動速度
    float        stickLx   = 0.0f;//Lスティックのx軸
    float        stickLy   = 0.0f;//Lスティックのy軸
    public int   playerHP;        //プレイヤーのHP
    int          EnemyAtk;        //敵の攻撃力
    // Update is called once per frame
    void Update()
    {
        MoveCube();       
    }
    void OnCollisionEnter(Collision collision)
    {
        //攻撃を受けたときの処理
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //Bulletのスクリプトを取得してEnemyAtkに代入
            EnemyAtk = collision.gameObject.GetComponent<Bullet>().EnemyATK; //BulletのEnemyATKをEnemyAtkに代入
            //プレイヤーのHPを減らす
            playerHP = playerHP - EnemyAtk;
        }
    }
    //プレイヤーの移動
    public void MoveCube()
    {
        stickLy = Input.GetAxis("Vertical");//Lスティックのy軸
        stickLx = Input.GetAxis("Horizontal");//Lスティックのx軸

        transform.Translate(Vector3.up * stickLy * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * stickLx * moveSpeed * Time.deltaTime);
    }    
}
