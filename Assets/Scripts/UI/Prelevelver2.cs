using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prelevelver2 : MonoBehaviour
{
    //public Slider progressBar;
    //public float minLoadTime = 3f; // 最小加载时间，单位：秒

    //private AsyncOperation asyncOperation;
    //private float loadStartTime;

    //private void Start()
    //{
    //    StartCoroutine(LoadSceneAsync());
    //}

    //private IEnumerator LoadSceneAsync()
    //{
    //    progressBar.value = 0f;

    //    // 启动异步加载场景
    //    asyncOperation = SceneManager.LoadSceneAsync("YourSceneName");
    //    asyncOperation.allowSceneActivation = false; // 禁止自动激活场景

    //    loadStartTime = Time.time;

    //    // 等待场景加载完成
    //    while (!asyncOperation.isDone)
    //    {
    //        float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // 获取加载进度，范围在0-1之间

    //        // 更新进度条
    //        progressBar.value = progress;

    //        // 加载时间已超过最小加载时间，并且进度条已经满
    //        if (Time.time - loadStartTime >= minLoadTime && progress >= 1f)
    //        {
    //            // 激活场景
    //            asyncOperation.allowSceneActivation = true;
    //        }

    //        yield return null;
    //    }
    //}
}
