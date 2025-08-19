using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyPrefab;//敵を定めるパブリック
    public GameObject player;     //プレイヤーを定めるパブリック  
    public float      start;      //始めにフラグを変えるタイミング
    public float      interval;   //フラグを再び変えるタイミング
    public int        MaxSpawn;   //敵の最大数を定める変数
    public int        SpawnCount; //敵の数を定める変数
           int        score;      //スコアを定める変数
    void Start()
    {
        //敵出現の間隔と最初の出現
        InvokeRepeating("Count", start, interval);
    }
    public void OnEnemyDestroyed()
    {
        SpawnCount--;
        score += 10; //敵を倒したらスコアを加算
    }
    public void Count()
    {
        if (SpawnCount < MaxSpawn)
        {
            //敵の数を生成するたびに増やす
            SpawnCount++;
            //敵を複製
            GameObject enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        }
    }
    void OnGUI()
    {
        //文字を表示するためのGUI.Labelを使用
        //Rect(x座標, y座標, 幅, 高さ)で位置とサイズを指定
        //energy.ToString("F1")でエネルギーの値を小数点以下1桁まで表示
        //エネルギーの表示 位置は画面の左上(10, 10)で、幅400、高さ50
        GUI.Label(new Rect(10, 50, 400, 50), "Score: " + score.ToString("F1"));
    }
}

