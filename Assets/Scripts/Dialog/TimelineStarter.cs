using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineStarter : MonoBehaviour {

    public GameObject whosTimeline;
    PlayableDirector pd;
    
	void Start () {
        pd = whosTimeline.GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (pd != null)
            pd.Play();
    }
}
