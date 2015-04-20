using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    public GameObject Player;

	void Start ()
    {
		transform.position = new Vector3(7.0f, 5.0f, transform.position.z);
	}

	void Update ()
    {
        float height = Grid.Instance.Height;
        float width = Grid.Instance.Width;
	    Vector2 origin = Grid.Instance.transform.position;

	    Vector2 cameraPosition = Player.transform.position;

        float cameraHeight = height;
        float cameraWidth = GetComponent<Camera>().aspect * height;

        if (cameraPosition.x < origin.x + cameraWidth / 2f)
            cameraPosition.x = origin.x + cameraWidth / 2f;
        if (cameraPosition.y < origin.y + cameraHeight / 2f)
            cameraPosition.y = origin.y + cameraHeight / 2f;
        if (cameraPosition.x > origin.x + width - cameraWidth / 2f)
            cameraPosition.x = origin.x + width - cameraWidth / 2f;
        if (cameraPosition.y > origin.y + height - cameraHeight / 2f)
            cameraPosition.y = origin.y + height - cameraHeight / 2f;

	    transform.position = new Vector3(cameraPosition.x, cameraPosition.y, transform.localPosition.z);
    }
}
