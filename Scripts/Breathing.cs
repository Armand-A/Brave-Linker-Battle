using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing : MonoBehaviour
{
    Vector3 baseScale; 

    float offset = 0;

    public int frameDelay = 1;

    public bool reverseCycle = false; 

    int dir = 1, frameSkip = 1;
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        if (reverseCycle){
            dir = -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (frameSkip == frameDelay){
            if (dir == 1) {
                if (offset < 0.05){
                    offset += 0.005f;
                } else {
                    dir = -1;
                }
           
            } else {
                if (offset > -0.05){
                    offset -= 0.005f;
                } else {
                    dir = 1;
                }
                
            }
            transform.localScale += new Vector3(0f, offset*0.025f, 0f);
            transform.position += new Vector3( 0f, (baseScale.y*offset*0.0625f), 0f );
            frameSkip = 0;
        } else {
            frameSkip++;
        }
        
    }
}
