using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class level4_5Fallinfvideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneName;

    void Awake()
    {
       // videoPlayer.Play();
       // videoPlayer.loopPointReached += CheckOver;
    }

    void Start()
    {

    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp) {

        SceneManager.LoadScene(sceneName);
    }
}
