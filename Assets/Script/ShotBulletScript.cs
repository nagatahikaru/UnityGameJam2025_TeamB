using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBulletScript : MonoBehaviour
{
    public GameObject playerbulletPrefab;//弾丸を定めるパブリック
    public GameObject enemy;             //プレイヤーを定めるパブリック
    public AudioClip  sound;             //射出時の効果音
    public float      BulletSpeed;       //弾丸の速度
    public float      desprn;            //弾丸が消えるタイミング
    public float      posisyonx;         //弾丸の位置を定める
    public float      energy;            //エネルギーを定める
    public float      consumableenergy;  //消費可能なエネルギー
    public float      acquisitionenergy; //弾丸を取得したときのエネルギー量
    void Start()
    {
        //プレイヤーを探知する
        enemy = GameObject.FindWithTag("Enemy");
    }
    void Update()
    {
        //エネルギーを定める
        if(energy >=0)
        {
            //エネルギーが0以上ならば時間経過で増加
            energy += Time.deltaTime * 10f; 
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if(energy>10.0f)
            {
                energy -= consumableenergy; //エネルギーを消費
                Shoot();      //弾丸を射出
            }
            else
            {
                return; //エネルギーが足りない場合は何もしない
            }            
        }
        if (energy < 0f)
        {
            energy = 0f; //エネルギーがマイナスにならないようにする
        }
        if (energy > 100f)
        {
            energy = 100f; //エネルギーが100を超えないようにする
        }
    }
    void OnGUI()
    {
        //文字を表示するためのGUI.Labelを使用
        //Rect(x座標, y座標, 幅, 高さ)で位置とサイズを指定
        //energy.ToString("F1")でエネルギーの値を小数点以下1桁まで表示
        //エネルギーの表示 位置は画面の左上(10, 10)で、幅400、高さ50
        GUI.Label(new Rect(10, 10, 400, 50), "Energy: " + energy.ToString("F1"));
    }
    void Shoot()
    {



        // プレイヤーの位置から、正面方向に 0.5f だけ前にずらす
        Vector3 spawnPos = transform.position + new Vector3(posisyonx, 0.0f,0.0f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);
        // 弾を生成
        GameObject bullet = Instantiate(playerbulletPrefab, spawnPos, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();//Rigidbodyを弾丸から取得して射出方向を決める


        // 弾速は自由に設定、directionは発射方向
        bulletRb.AddForce(Vector2.right * BulletSpeed);
        // 発射音を出す
        AudioSource.PlayClipAtPoint(sound, transform.position);

        // ５秒後に砲弾を破壊する
        Destroy(bullet, desprn);
    }  
}
