using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour
{
    public GameObject[] players;
    public Vector3 up = Vector3.forward;
    public Vector3 right = Vector3.right;
    public Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (players.Length > 0)
        {
            Vector3 average = Vector3.zero;
            float dist = 0;
            foreach (GameObject p in players)
            {
                average += p.transform.position;
                foreach (GameObject p2 in players)
                {
                    if (p != p2)
                    {
                        if (Vector3.Distance(p.transform.position, p2.transform.position) > dist)
                            dist = Vector3.Distance(p.transform.position, p2.transform.position);
                    }
                }
            }
            average /= players.Length;
            transform.position = Vector3.Lerp(transform.position, average + (Vector3.one + Vector3.up) * 8, 0.2f);
            transform.LookAt(transform.position - (Vector3.one + Vector3.up));

            if (players.Length > 1)
                camera.fieldOfView = dist + 40;
            else
                camera.orthographicSize = Mathf.Lerp(Camera.current.fieldOfView, 40, 1f);

            right = transform.right;
            up = transform.forward;
        }
    }
}