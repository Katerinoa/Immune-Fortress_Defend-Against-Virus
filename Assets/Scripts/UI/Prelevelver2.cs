using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prelevelver2 : MonoBehaviour
{
    //public Slider progressBar;
    //public float minLoadTime = 3f; // ��С����ʱ�䣬��λ����

    //private AsyncOperation asyncOperation;
    //private float loadStartTime;

    //private void Start()
    //{
    //    StartCoroutine(LoadSceneAsync());
    //}

    //private IEnumerator LoadSceneAsync()
    //{
    //    progressBar.value = 0f;

    //    // �����첽���س���
    //    asyncOperation = SceneManager.LoadSceneAsync("YourSceneName");
    //    asyncOperation.allowSceneActivation = false; // ��ֹ�Զ������

    //    loadStartTime = Time.time;

    //    // �ȴ������������
    //    while (!asyncOperation.isDone)
    //    {
    //        float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // ��ȡ���ؽ��ȣ���Χ��0-1֮��

    //        // ���½�����
    //        progressBar.value = progress;

    //        // ����ʱ���ѳ�����С����ʱ�䣬���ҽ������Ѿ���
    //        if (Time.time - loadStartTime >= minLoadTime && progress >= 1f)
    //        {
    //            // �����
    //            asyncOperation.allowSceneActivation = true;
    //        }

    //        yield return null;
    //    }
    //}
}
