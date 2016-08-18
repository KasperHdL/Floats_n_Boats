using UnityEngine;
using System.Collections;

public class TitleLetters : MonoBehaviour {

    [SerializeField] private Transform[] letters; 
    [SerializeField] private float floatingAmount;
    [SerializeField] private float floatingDuration;
    [SerializeField] private float floatingRandomDuration;
    
    private Vector3[] letterInitialPos;
    private Vector3[] letterEndPos;
    private Vector3[] letterStartPos;
    private float[] letterDuration;
    private float[] letterStartTime;

    private bool[] letterHaveAnimatedIn;
    private bool lettersAnimatingOut = false;
    
    private Vector3 position;

    [Header("Animating In")]
    [SerializeField] private Vector3 startOffset;
    [SerializeField] private float animateInDuration;
    [SerializeField] private float animateOutDuration;

    [SerializeField] private AnimationCurve curveAnimateIn;
    [SerializeField] private AnimationCurve curveAnimateFloat;
    [SerializeField] private AnimationCurve curveAnimateOut;
    
	void Start () {
        position = transform.position;
        
        letterInitialPos        = new Vector3[letters.Length];
        letterHaveAnimatedIn    = new bool[letters.Length];
        letterStartPos          = new Vector3[letters.Length];
        letterEndPos            = new Vector3[letters.Length];
        letterDuration          = new float[letters.Length];
        letterStartTime         = new float[letters.Length];

        
        for(int i = 0;i < letters.Length ; i++){
            letterHaveAnimatedIn[i] = false;
            letterInitialPos[i]     = letters[i].position;
            letterStartPos[i]       = letters[i].position + startOffset;
            letterEndPos[i]         = letters[i].position + Random.insideUnitSphere * floatingRandomDuration;
            letterStartTime[i]      = Time.time;
            letterDuration[i]       = animateInDuration + Random.value * floatingRandomDuration;
        }

	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0;i < letters.Length; i++){
            float t = (Time.time - letterStartTime[i]) / letterDuration[i];
            
            if(t > 1){
                letterStartPos[i] = letters[i].position;
                letterEndPos[i] = letterInitialPos[i] + Random.insideUnitSphere * floatingAmount;
                letterDuration[i] = floatingDuration + Random.value * floatingRandomDuration;
                letterStartTime[i] = Time.time;

                if(letterHaveAnimatedIn[i] == false)
                    letterHaveAnimatedIn[i] = true;
                
                t = 0;
            }

            float ct;
            if(lettersAnimatingOut)
                ct = curveAnimateOut.Evaluate(t);
            else if(letterHaveAnimatedIn[i])
                ct = curveAnimateFloat.Evaluate(t);
            else
                ct = curveAnimateIn.Evaluate(t);

            letters[i].position = Vector3.Lerp(letterStartPos[i], letterEndPos[i], ct); 
        }
	}

    public void GameStarted(){

        for(int i = 0 ; i< letterDuration.Length; i++){
            letterStartPos[i]       = letters[i].position;
            letterEndPos[i]         = letterInitialPos[i] + Random.insideUnitSphere * floatingAmount;
            letterStartTime[i]      = Time.time;
            letterDuration[i]       = animateOutDuration;
        }

        lettersAnimatingOut = true;
        StartCoroutine(destroyMe(animateOutDuration));
        
        
    }

    IEnumerator destroyMe(float duration){
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}

