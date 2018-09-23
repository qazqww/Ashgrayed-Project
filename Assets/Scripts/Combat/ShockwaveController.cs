using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveController : MonoBehaviour {

    float size;

    Transform tr;
    CircleCollider2D circle;
	
	void Start () {
        tr = GetComponent<Transform>();
        circle = GetComponent<CircleCollider2D>();
        size = 1f;
	}
	
	void Update () {
        circle.radius += Time.deltaTime * 0.08f;
        size += Time.deltaTime * 4f;
        tr.localScale = new Vector3(size, size, 1f);
	}
}
