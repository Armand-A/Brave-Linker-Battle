using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public static GameFlow GF;
    
    public GameObject map, gameBoard, hero, logo, tileGroup1, tileGroup2, tileGroup3, cta;

    GameObject hint1, hint2, hint3;

    Animator map_ANM, gameBoard_ANM, hero_ANM, logo_ANM;

    public int state = 0;
    public bool canDraw;
    public bool canClickCTA;

    void Start()
    {
        GF = this; 
        map_ANM = map.GetComponent<Animator>();
        gameBoard_ANM = gameBoard.GetComponent<Animator>();
        hero_ANM = hero.GetComponent<Animator>();
        logo_ANM = logo.GetComponent<Animator>();
        hint1 = tileGroup1.transform.GetChild(0).gameObject;
        hint2 = tileGroup2.transform.GetChild(0).gameObject;
        hint3 = tileGroup3.transform.GetChild(0).gameObject;
    }

    public void phaseOne(){
        state = 1;
        tileGroup1.SetActive(true);
        canDraw = true;
    }

    public void firstLine(){
        state = 2;
        tileGroup1.SetActive(false);
        canDraw = false;
        gameBoard_ANM.Play("Board_Anim1", 0, 0);
        hero_ANM.Play("Hero_Attack", 0, 0);
        StartCoroutine(handleTransition());
    }

    public void secondLine(){
        state = 4;
        tileGroup2.SetActive(false);
        canDraw = false;
        gameBoard_ANM.Play("Board_Anim2", 0, 0);
        hero_ANM.Play("Hero_Attack", 0, 0);
    }
    public void lastPhase(){
        state = 5;
        tileGroup3.SetActive(true);
        canDraw = true;
    }

    public void thirdLine(){
        state = 6;
        tileGroup3.SetActive(false);
        canDraw = false;
        gameBoard_ANM.Play("Board_Anim3", 0, 0);
        hero_ANM.Play("Hero_Attack", 0, 0);
    }

    public void enableHints(){
        hint1.SetActive(true);
        hint2.SetActive(true);
        hint3.SetActive(true);
    }

    public void disableHints(){
        hint1.SetActive(false);
        hint2.SetActive(false);
        hint3.SetActive(false);
    }

    void moveHero(){
        hero_ANM.Play("Hero_Run", 0, 0);
        map_ANM.Play("BG_Shift_2", 0, 0);
    }

    IEnumerator handleTransition(){
        yield return new WaitForSeconds(1f);
        moveHero();
        yield return new WaitForSeconds(1.1f);
        state = 3;
        tileGroup2.SetActive(true);
        canDraw = true;
    }

    public IEnumerator handleEnding(){
        state = 7;
        yield return new WaitForSeconds(1f);
        hero_ANM.Play("Hero_End_Run", 0, 0);
        yield return new WaitForSeconds(1.1f);
        cta.SetActive(true);
        logo_ANM.Play("Logo_CTA", 0, 0);
        canClickCTA = true;
    }

    public IEnumerator recieveHit(){
        yield return new WaitForSeconds(0.185f);
        hero_ANM.Play("Hero_Hit", 0, 0);
        yield return new WaitForSeconds(0.75f);
        gameBoard_ANM.Play("Board_Anim2_B", 0, 0);
        yield return new WaitForSeconds(0.5f);
        lastPhase();
    }
}
