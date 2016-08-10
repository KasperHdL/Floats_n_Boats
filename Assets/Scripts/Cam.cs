using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour
{
    public GameObject[] players;
    public Vector3 up = Vector3.forward;
    public Vector3 right = Vector3.right;
    public Camera camera;

    public Vector3 menuPos;
    public Quaternion menuRotation;
    private float menuFOV;

    [SerializeField] private float maxSpeed = 0.5f;
    [SerializeField] private float maxFovChange = 0.5f;


    [SerializeField] private float V3UpMax = 8f;
    [SerializeField] private float V3UpMin = 4f;

    [SerializeField] private float V3OneMin = 10f;
    [SerializeField] private float V3OneMax = 20f;
    
    [SerializeField] private float FOVMin = 25f;
    [SerializeField] private float FOVMax = 30f;


    private void Start()
    {
        

        menuPos = transform.position;
        menuRotation = transform.rotation;

        camera = GetComponent<Camera>();
        menuFOV = camera.fieldOfView;

        this.enabled = false;
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
            
            float scaledDist = Mathf.Clamp((dist - 10) / 50, 0f, 1f);
            
            float V3One = scaledDist * V3OneMax + V3OneMin;
            float V3Up = scaledDist * (V3UpMax - V3UpMin) + V3UpMin;
            float FOV = scaledDist * FOVMax + FOVMin;
            
            Vector3 desiredPosition = average + Vector3.one * V3One + Vector3.up * V3Up;

            Vector3 delta = desiredPosition - transform.position;
            if(delta.magnitude > maxSpeed)
                delta = delta.normalized * maxSpeed;

            
            transform.position = Vector3.Lerp(transform.position, transform.position + delta, 0.5f);


            Quaternion desiredRotation = Quaternion.LookRotation(-(Vector3.one + Vector3.up));
                
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.1f);

            if (players.Length > 1){
                float desiredFOV = FOV;
                float deltaFOV = desiredFOV - camera.fieldOfView;
                if(Mathf.Abs(deltaFOV) > maxFovChange)
                    deltaFOV = Mathf.Sign(deltaFOV) * maxFovChange;
                camera.fieldOfView = camera.fieldOfView + deltaFOV;
            }else
                camera.orthographicSize = Mathf.Lerp(Camera.current.fieldOfView, 40, 1f);

            right = transform.right;
            up = transform.forward;
        }
    }

    public void lerpToMenu(float animationLength){
        StartCoroutine(routine_lerpToMenu(animationLength ));
    }

    IEnumerator routine_lerpToMenu(float length){
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        float startFOV = camera.fieldOfView;

        float startTime = Time.time; 
        float t = 0;
        
        while(startTime + length > Time.time){
            
            t = (Time.time - startTime) / length;
            transform.position = Vector3.Lerp(startPosition, menuPos, t);
            transform.rotation = Quaternion.Lerp(startRotation, menuRotation, t);
            camera.fieldOfView = Mathf.Lerp(startFOV, menuFOV, t);
            
            yield return null;
        }
        
        transform.position = menuPos;
        transform.rotation = menuRotation;

        startTime = Time.time;
        length = 3f; 
        
        while(startTime + length > Time.time){
            t = (Time.time - startTime) / length;
            Debug.Log(t);
                    this.enabled = false; 

            yield return null;
        }
        
        this.enabled = true; 
    } 
}