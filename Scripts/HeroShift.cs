using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShift : MonoBehaviour
{
    public GameObject monster1, monster2, monster3;
    public void finishRun(){
        if (GameFlow.GF.state == 0){
            GameFlow.GF.phaseOne();
        }
    }

    public void midAttackTrigger(){
        if (GameFlow.GF.state == 2){
            monster1.transform.GetChild(0).gameObject.SetActive(true);
            monster1.GetComponent<Animator>().Play("Hornskull_Fade", 0, 0);
        } else if (GameFlow.GF.state == 4){
            monster2.transform.GetChild(0).gameObject.SetActive(true);
            monster2.GetComponent<Animator>().Play("Hornskull_Fade", 0, 0);
        } else if (GameFlow.GF.state == 6){
            monster3.transform.GetChild(0).gameObject.SetActive(true);
            monster3.GetComponent<Animator>().Play("ox_fade", 0, 0);
        }
    }
    public void finishAttack(){
        if (GameFlow.GF.state == 6){
            StartCoroutine(GameFlow.GF.handleEnding());
        }
    }
    public void readyNextState(){
        if (GameFlow.GF.state == 4){
            StartCoroutine(activateHit());
        }
    }

    IEnumerator activateHit(){
        GameFlow.GF.state = -1;
        yield return new WaitForSeconds(0.25f);
        monster3.GetComponent<Animator>().Play("ox_attack", 0, 0);
        StartCoroutine(GameFlow.GF.recieveHit());
    }
}
