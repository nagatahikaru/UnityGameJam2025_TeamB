using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    public  GameObject bulletPrefab;//弾丸を定めるパブリック
    public  GameObject player;      //プレイヤーを定めるパブリック
    public  AudioClip  sound;       //射出時の効果音
    public  float      BulletSpeed; //弾丸の速度   
    public  float      start;       //始めに打つタイミング
    public  float      interval;    //打ってから再び打つ間隔
    public  float      flagstart;   //始めにフラグを変えるタイミング
    public  float      flaginterval;//フラグを再び変えるタイミング
    public  float      desprn;      //弾丸が消えるタイミング
    private int        BulletState; //ランダムに方向のステートを定める
    public  Vector2    direction;   //方向        
    bool               bulletflag;  //弾丸のフラグ
    void Start()
    {
        // 指定したメソッドを、指定した時間（単位；秒）から、指定した間隔（単位；秒）で繰り返し実行する。
        InvokeRepeating("bullet", start, interval);
        InvokeRepeating("Flag", flagstart, flaginterval);//x秒後にフラグを変えy秒ごとにフラグを変更する
        //プレイヤーを探知する
        player = GameObject.FindWithTag("Player");
    }
    void Flag()
    {
        bulletflag = true;
    }   
    void bullet()
    {
        BulletManager();
        if (bulletflag == true)
        {
            BulletState = Random.Range(0, 2);
            bulletflag = false;
        }
        Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);//弾丸射出時の角度を９０度にしてまっすぐ出しているように見せる
       //bulletは発射物
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();//Rigidbodyを弾丸から取得して射出方向を決める
        bullet.GetComponent<Bullet>().SetDirection(direction);

        // 弾速は自由に設定、directionは発射方向
        bulletRb.AddForce(direction * BulletSpeed);
        // 発射音を出す
           AudioSource.PlayClipAtPoint(sound, transform.position);

        // ５秒後に砲弾を破壊する
        Destroy(bullet, desprn);
    }

    void BulletManager()
    {
        //方向をランダムに定める
        switch(BulletState)
        {
            case 0:
                //プレイヤーに向けて方向を定める
                direction =  (player.transform.position - transform.position).normalized;
                break;
            case 1:
                //ワールド座標を起点として方向を左に定める
                direction = Vector2.left;
                break;
        }
    }
}