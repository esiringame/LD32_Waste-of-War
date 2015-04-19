using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public GameObject player;

	private int height;
	private int width;

	void Start () {
		transform.position = new Vector3(player.transform.position.x, 4.5f, transform.position.z);
	}

	void Update () {
		height = Grid.Instance.Height;
		width = Grid.Instance.Width;

		if (player.transform.position.x > 5 && player.transform.position.x < width - 5)
			transform.position = new Vector3(player.transform.position.x, 4.5f, transform.position.z);
	}
}
