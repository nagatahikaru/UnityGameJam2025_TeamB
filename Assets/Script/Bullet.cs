using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2   direction;//弾丸が進む方向
    private Rigidbody rd;       // Rigidbodyを定義して弾丸の動きを制御する
    public  float      speed;   //弾丸の速度
    public  int EnemyATK;       //敵の攻撃力

    void Awake()
    {
      rd = GetComponent<Rigidbody>();
    }   
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;//弾丸が進む方向
    }
    private void FixedUpdate()
    {
        rd.velocity = direction * speed;   
    }
    void OnCollisionEnter(Collision collision)
    {
        //タグを検知してプレイヤーの攻撃タグだったら
        if (collision.gameObject.CompareTag("ATK"))
        {
            //自身を削除
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }

}
