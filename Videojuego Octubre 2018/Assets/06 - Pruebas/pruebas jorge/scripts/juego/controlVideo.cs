using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class controlVideo : MonoBehaviour
{
    VideoPlayer Video;
    bool videoVisto;
    // Start is called before the first frame update
    void Start()
    {
        Video = GetComponent<VideoPlayer>();
        Video.Play();
        Video.loopPointReached += EndReached;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        print("VIDEO STIOP");
        gameObject.SetActive(false);
    }

    public void Skip()
    {
        gameObject.SetActive(false);
    }
}
