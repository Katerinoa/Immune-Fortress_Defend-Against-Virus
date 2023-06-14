using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PreLevel1 : MonoBehaviour
{
    public string sceneName; // 要加载的场景名称
    public VideoPlayer videoPlayer; // 视频播放器组件
    private AsyncOperation sceneLoadOperation; // 场景加载操作

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd; // 注册视频播放结束事件
        videoPlayer.Play(); // 开始播放视频

        StartCoroutine(LoadSceneAsync()); // 异步加载场景
    }

    IEnumerator LoadSceneAsync()
    {
        sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName); // 开始异步加载场景
        sceneLoadOperation.allowSceneActivation = false; // 禁止场景自动激活

        yield return sceneLoadOperation; // 等待场景加载完成
    }

    void OnVideoEnd(VideoPlayer player)
    {
        sceneLoadOperation.allowSceneActivation = true; // 允许场景激活
    }
}
