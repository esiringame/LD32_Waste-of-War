using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public GameObject player;

	private int height;
	private int width;

	void Start () {
		transform.position = new Vector3(7.0f, 5.0f, transform.position.z);
	}

	void Update () {
		height = Grid.Instance.Height;
		width = Grid.Instance.Width;

		if (player.transform.position.x > 7 && player.transform.position.x < width - 7)
			transform.position = new Vector3(player.transform.position.x, 5.0f, transform.position.z);
	}
}
