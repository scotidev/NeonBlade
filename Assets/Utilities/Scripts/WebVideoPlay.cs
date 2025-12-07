using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class WebGLVideoFix : MonoBehaviour
{
    void Start()
    {
        VideoPlayer vp = GetComponent<VideoPlayer>();

        string videoName = "BgVideo.mp4";
        vp.url = Path.Combine(Application.streamingAssetsPath, videoName);

        vp.Prepare();
        vp.prepareCompleted += (player) =>
        {
            player.Play();
        };
    }
}