using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem)), ExecuteInEditMode]

public class partcilesrevser : MonoBehaviour
{
    ParticleSystem s;
    public float curTime = 0;
    public float speed;
    public uint seed;

    // Use this for initialization
    void Start()
    {
        s = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (curTime <= 0)
        {
            curTime = s.main.duration;
        }
        s.randomSeed = seed;
        s.Simulate(curTime);
        curTime -= speed * Time.deltaTime;
    }
}