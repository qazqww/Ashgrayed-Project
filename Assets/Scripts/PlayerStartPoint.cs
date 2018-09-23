using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    public Vector2 startDirection;

    private PlayerMovement player;
    private CameraFollow camera;

	void Start () {
        player = FindObjectOfType<PlayerMovement>();
        player.transform.position = transform.position;
        player.movement = startDirection;

        camera = FindObjectOfType<CameraFollow>();
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
	}
}
