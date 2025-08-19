using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーンを扱うとき必要

public class ScenesChangeInGame : MonoBehaviour
{
    public string              GameOverScene;  // 遷移先のシーン名
    public string              GameClearScene; // 遷移先のシーン名
        //⇩目的のスクリプト名  ⇩変数名
    public Player_Script       player_HP;      // PlayerのHPを管理するスクリプト
    　　　 float               timelimit;      // ゲームの時間制限

    void Update()
    {
        // 時間制限を設定
        // Time.timeSinceLevelLoadはゲーム開始からの経過時間を取得する
        // ゲーム開始からの経過時間を取得
        timelimit = Time.timeSinceLevelLoad;
        // 時間制限が過ぎたらゲームクリアシーンに遷移
        if (timelimit >= 180f) // 60秒経過したら
        {
            SceneManager.LoadScene(GameClearScene);
        }
        // PlayerのHPが0以下になったらシーンを遷移
        if (player_HP.playerHP <= 0f)
        {            
            SceneManager.LoadScene(GameOverScene);
        }
        

        
    }
}
