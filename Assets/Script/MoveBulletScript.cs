using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MoveBulletScript : MonoBehaviour
{   
    public  GameObject bulletPrefab;     // 弾のプレハブ
    private Rigidbody  rd;               // Rigidbodyコンポーネントを格納する変数
    public  float      speed;            // 弾の移動速度
    void Awake()
    {
        rd = GetComponent<Rigidbody>();
    }
   
    private void FixedUpdate()
    {
        rd.velocity = Vector2.right * speed;
        // 弾の生成位置を調整,Z軸方向に90度回転
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }
    void OnCollisionEnter(Collision collision)
    {
        //タグを検知してプレイヤーの攻撃タグだったら
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //自身を削除
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {           
            Destroy(gameObject);                       
        }

    }
    void OnTriggerEnter(Collider other)
    {
        
    }

}
