using UnityEngine;
using System.Collections;
using System;

public class Button : MonoBehaviour
{
    [Range(50,300)] public int rotationMultiplier;
    public Color occupiedColor;

    private float rotation;
    private Quaternion startRotation;
    private MeshRenderer outerShell;
    private MeshRenderer innerShell;

    // Use this for initialization
    void Start()
    {

        startRotation = transform.rotation;

        innerShell = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        outerShell = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rotation += Time.deltaTime * rotationMultiplier;

        transform.rotation = startRotation * Quaternion.Euler(0, rotation, 0);
    }

    public IEnumerator Occupy()
    {
        Color oc = outerShell.material.color;
        Color ic = innerShell.material.color;

        float t = 0f;

        while (t < 1f)
        {
            t += 1.5f * Time.fixedDeltaTime;

            outerShell.material.color = Color.Lerp(oc, occupiedColor, t);
            innerShell.material.color = Color.Lerp(ic, occupiedColor, t);

            yield return new WaitForFixedUpdate();
        }
    }
}
