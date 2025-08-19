using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    int                 EnemyState = 0;   //敵の状態を定める変数
    public GameObject   bullet;           //弾丸を定めるパブリック
    public GameObject   player;           //プレイヤーを定めるパブリック
    public EnemyManager enemyManager;     //敵のマネージャー
    Vector2             targetPosition;   //目的地のポジション
    float               duration =   0.3f;//カーブの時間
    float               t =          0.0f;//カーブの中点
    Vector2             basePosition;     //自身のポジション変数
    bool                Moveflag =   true;//移動フラグ


    void Start()
    {
        //敵のマネージャーを探知する通知用変数
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        //プレイヤーのタグを探知
        player = GameObject.FindWithTag("Player");
        //自分のポジションを変数に代入
        basePosition = transform.position;
    }
    void Update()
    {
        MoveEnemy();
        ManageState();
    }
    private void OnDestroy()
    {
        if (enemyManager != null)
        {
            enemyManager.OnEnemyDestroyed();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //タグを検知してプレイヤーの攻撃タグだったら
        if (collision.gameObject.CompareTag("ATK"))
        {
            //自身を削除
            Destroy(gameObject);
        }
    }
    void MoveEnemy()
    {
        //目的地のxyをランダムで決める
        if (Moveflag == true)
        {   //xの位置をランダム決定
            float x = Random.Range(-2.0f, 9.5f);
            //yの位置をランダムで決める
            float y = Random.Range(-4.0f, 6.0f);

            targetPosition = new Vector2(x, y);
            EnemyState = Random.Range(0, 3);
            Moveflag = false;
        }
    }

    void ManageState()
    {
        //波長　振幅
        float yOffset = Mathf.Sin(Time.time * 3f) * 4.0f;
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;//
        t = Mathf.Min(t, 1f);//カーブの中点     
        float speed = 0.5f;
        t += Time.deltaTime / duration;

        switch (EnemyState)
        {
            case 0://直線で発射
                transform.Translate(direction * speed * Time.deltaTime);
                break;

            case 1://波状に発射
                transform.position = new Vector2(transform.position.x, basePosition.y + yOffset);
                transform.Translate(direction * speed * Time.deltaTime);
                break;

            case 2://曲線で発射
                transform.position = Vector2.Lerp(transform.position, targetPosition, t);
                if (EnemyState == 2 && Vector2.Distance(transform.position, targetPosition) < 0.1f)
                {
                    Moveflag = true;//フラグリセット
                    t = 0f;//カーブリセット
                    basePosition = transform.position; // 位置を更新
                }
                break;
        }

    }
}
