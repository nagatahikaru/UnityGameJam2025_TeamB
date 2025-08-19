using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーンを扱うとき必要

public class ScenesChang : MonoBehaviour
{
    public string SceneName;    // 遷移先のシーン名

    void Update()
    {
        // スペースキーが押されたらシーンを切り替える
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
