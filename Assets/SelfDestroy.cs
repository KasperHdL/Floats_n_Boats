using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {
    private MeshRenderer[] chars;

	// Use this for initialization
	void Start () {
        chars = new MeshRenderer[transform.childCount];


        for(int i = 0; i < transform.childCount;i++)
        {
            chars[i] = transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();
        }

        StartCoroutine(Dissapear());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(3f);

        float t = 1f;
        while (true)
        {
            t += Time.deltaTime / 2;
            transform.position = transform.position + (Vector3.forward * t);

            Debug.Log(t);

            if (t > 1.5f)
                break;

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
