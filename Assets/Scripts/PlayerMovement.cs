using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Animations;




public class PlayerMovement : MonoBehaviour {

    private Tilemap obstacleTilemap;
    Vector2 targetCell;
    Vector2 startCell;
    Vector2 newPos;
    public Transform target;

    [SerializeField]
    float moveTime = 1f;

    bool targetObstacle;
    bool onMove = false;
    bool input = false;

    int xMove;
    int yMove;

    Animator anime;

   



    void Start() {

        Vector2 temp = transform.position;
        target.position = temp += new Vector2(0, -1);
        newPos = transform.position;
        obstacleTilemap = GameObject.FindGameObjectWithTag("tile").GetComponent<Tilemap>();
        anime = GetComponent<Animator>();

    }

    void Update() {

        Inputs();
        
        if(input == true) FindTargetCell();

        CheckTargetCell();

        if(onMove == false)Animate();

        if (input == true) Move();

       

    }

    void Inputs() {

        xMove = (int)(Input.GetAxisRaw("Horizontal"));
        yMove = (int)(Input.GetAxisRaw("Vertical"));

        if (xMove != 0) { yMove = 0; }
        
        if (xMove!= 0 || yMove != 0) {
            input = true;
        }

    }

    void Animate()
    {

        if(xMove == 0 && yMove == 1)
        {
            
            anime.ResetTrigger("down");
            anime.ResetTrigger("left");
            anime.ResetTrigger("right");
            anime.SetTrigger("up");
        }
        else if (xMove == 1 && yMove == 0)
        {
            
            anime.ResetTrigger("down");
            anime.ResetTrigger("left");
            anime.ResetTrigger("up");
            anime.SetTrigger("right");
        }
        else if (xMove == 0 && yMove == -1)
        {
            
            anime.ResetTrigger("left");
            anime.ResetTrigger("up");
            anime.ResetTrigger("right");
            anime.SetTrigger("down");
        }
        else if (xMove == -1 && yMove == 0)
        {
            
            anime.ResetTrigger("down");
            anime.ResetTrigger("up");
            anime.ResetTrigger("right");
            anime.SetTrigger("left");
        }
    }

    void FindTargetCell() {

        Vector2 temp = transform.position;

        target.position = temp + new Vector2(xMove, yMove);

    }

    void CheckTargetCell() {

        targetObstacle = getCell(obstacleTilemap, target.position) != null;

    }

    void Move() {

        if(targetObstacle == true && onMove == false) {

            newPos = target.position;
            StartCoroutine(SmoothMovement(newPos));

        }
        input = false;
        

    }

    private IEnumerator SmoothMovement(Vector3 end) {

        onMove = true;


        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        //float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistance > float.Epsilon) {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, moveTime* Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        onMove = false;
    }

    private TileBase getCell(Tilemap tilemap, Vector3 cellWorldPos) {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }
}