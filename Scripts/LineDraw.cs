using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    LineRenderer Line;
    EdgeCollider2D edgeCollider; 

    List<Collider2D> currentCollisions;
    public float lineWidth = 0.04f;
    public float minimumVertexDistance = 0.1f;

    private bool isLineStarted, wait;

    private int i;

    void Start()
    {
        Line = this.GetComponent<LineRenderer>();
        edgeCollider = this.GetComponent<EdgeCollider2D>();

        currentCollisions = new List<Collider2D>();

        // set the color of the line
        //Line.startColor = Color.yellow;
        //Line.endColor = Color.yellow;

        // set width of the renderer
        Line.startWidth = lineWidth;
        Line.endWidth = lineWidth;

        isLineStarted = false;
        Line.positionCount = 0;
    }

    void Update()
    {
        
        if (GameFlow.GF.canDraw){
            if(Input.GetMouseButtonDown(0) && !wait)
            {
                Line.positionCount = 0;
                Vector3 mousePos = GetWorldCoordinate(Input.mousePosition);
                
                Line.positionCount = 2;
                Line.SetPosition(0, mousePos);
                Line.SetPosition(1, mousePos);
                isLineStarted = true;

                GameFlow.GF.disableHints();
            }

            if (Input.GetMouseButton(0) && isLineStarted)
            {
                Vector3 currentPos = GetWorldCoordinate(Input.mousePosition);
                float distance = Vector3.Distance(currentPos, Line.GetPosition(Line.positionCount - 1));
                if (distance > minimumVertexDistance)
                {
                    //Debug.Log(distance);
                    UpdateLine();
                    defineEdgeCollider(Line);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                clearLine();
            }
        }
        if (GameFlow.GF.canClickCTA && Input.GetMouseButtonDown(0)){
            Luna.Unity.Playable.InstallFullGame();
        }
        

        
    }

    void clearLine(){
        isLineStarted = false;
        currentCollisions.Clear();
        Line.positionCount = 2;
        Line.SetPosition(0, new Vector3(0, 0, 0));
        Line.SetPosition(1, new Vector3(0, 0, 0));
        defineEdgeCollider(Line);
        GameFlow.GF.enableHints();
        //wait = true;
        //StartCoroutine(vanishLine());
    }

    /*IEnumerator vanishLine(){
        yield return new WaitForSeconds(0.3f);
        currentCollisions.Clear();
        Line.positionCount = 2;
        Line.SetPosition(0, new Vector3(0, 0, 0));
        Line.SetPosition(1, new Vector3(0, 0, 0));
        defineEdgeCollider(Line);
        GameFlow.GF.enableHints();
        wait = false;
    }*/
    private void UpdateLine()
    {
        Line.positionCount++;
        Line.SetPosition(Line.positionCount - 1, GetWorldCoordinate(Input.mousePosition));
        /*if (Line.positionCount < 40){
            Line.positionCount++;
            Line.SetPosition(Line.positionCount - 1, GetWorldCoordinate(Input.mousePosition));
        } else if (i < 40){
            Line.SetPosition(Line.positionCount - 1, GetWorldCoordinate(Input.mousePosition));
            Line.SetPosition(i, Line.GetPosition(i));
            i++;
        } else if (i >= 40){
            i = 0;
            Line.SetPosition(Line.positionCount - 1, GetWorldCoordinate(Input.mousePosition));
            Line.SetPosition(i, GetWorldCoordinate(Input.mousePosition));
        }*/

    }

    private Vector3 GetWorldCoordinate(Vector3 mousePosition)
    {
        Vector3 mousePos = new Vector3(mousePosition.x, mousePosition.y, 1);
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void defineEdgeCollider(LineRenderer lineRenderer){
        List<Vector2> edges = new List<Vector2>();
 
        for(int point = 0; point<lineRenderer.positionCount; point++)
        {
            Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }
 
        edgeCollider.points = edges.ToArray();
    }

    void OnTriggerEnter2D(Collider2D collision){
        currentCollisions.Add(collision);
        Debug.Log(currentCollisions.Count);
        if (GameFlow.GF.state == 1 && currentCollisions.Count == 3){
            clearLine();
            GameFlow.GF.firstLine();
        } else if (GameFlow.GF.state == 3 && currentCollisions.Count == 5){
            clearLine();
            GameFlow.GF.secondLine();
        } else if (GameFlow.GF.state == 5 && currentCollisions.Count == 4){
            clearLine();
            GameFlow.GF.thirdLine();

        }
        
    }

    /*
    void OnTriggerExit2D(Collider2D collision){
        currentCollisions.Add(collision);
        //Debug.Log(currentCollisions.Count);
        /*if (GameFlow.GF.state == 1 && currentCollisions.Count == 3){
            GameFlow.GF.firstLine();
            clearLine();
        } else if (GameFlow.GF.state == 3 && currentCollisions.Count == 5){
            GameFlow.GF.secondLine();
            clearLine();
        }
        
    }*/
}

//Adapted from Gyanendu Shekhar and ZeroKelvin