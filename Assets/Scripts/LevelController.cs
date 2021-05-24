using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    public ConductorLooping conductor;
    void Start()
    {
        conductor = GetComponentInChildren<ConductorLooping>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public bool isOnBeat()
    {
        float marker = conductor.songPositionInBeats % 1.0f;
        Debug.Log(marker);
        return (marker > 0.9 || marker < 0.4);
    }
}
