using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar : MonoBehaviour
{

    [SerializeField]
    float moveTime = 0.05f;

    Tilemap ground;

    Animator anime;

    Vector2Int startPos;
    Vector2Int targetPos;

    public List<Path> openList = new List<Path>();
    List<Vector2Int> alreadyChecked = new List<Vector2Int>();
    List<Path> closedList = new List<Path>();

    bool up;
    bool down;
    bool left;
    bool right;

    public bool running = true;
    Vector3 tempStart = new Vector3(1.0f, 1.0f, 5.0f);



    Vector3 exit;

    private void Start() {

        ground = GameObject.FindGameObjectWithTag("tile").GetComponent<Tilemap>();
        anime = GetComponent<Animator>();

    }


    private void ResetLists() {
        openList.Clear();
        closedList.Clear();
        alreadyChecked.Clear();
    }

    private void GetStartPositions(Vector3 pos) {

        tempStart = ground.WorldToCell(transform.position);
        startPos = Vector2Int.RoundToInt(tempStart);

        Vector3 tempTarget = ground.WorldToCell(pos);
        targetPos = Vector2Int.RoundToInt(tempTarget);

    }

    private int ManhattanDistance(Vector2Int a, Vector2Int b) {
        checked {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }

    private void IntiateAstar() {
        float b = ManhattanDistance(startPos, targetPos);
        openList.Add(new Path(0, b, null, startPos));

    }

    private void BuildTiles(Path z) {
        if (up == true) {
            int dist = ManhattanDistance(z.pos, targetPos);
            openList.Add(new Path(z.g + 1, dist, z, z.pos + new Vector2Int(0, 1)));
        }

        if (down == true) {
            int dist = ManhattanDistance(z.pos, targetPos);
            openList.Add(new Path(z.g + 1, dist, z, z.pos + new Vector2Int(0, -1)));
        }

        if (left == true) {
            int dist = ManhattanDistance(openList[0].pos, targetPos);
            openList.Add(new Path(z.g + 1, dist, z, z.pos + new Vector2Int(-1, 0)));

        }
        if (right == true) {
            int dist = ManhattanDistance(openList[0].pos, targetPos);
            openList.Add(new Path(z.g + 1, dist, z, z.pos + new Vector2Int(1, 0))); 
        }
    }

    void SetBool(Vector2Int aiPos, int x, int y, string str) {

        Vector3Int temp = Vector3Int.RoundToInt(aiPos + new Vector2(x, y));
        Vector2Int l = new Vector2Int(temp.x, temp.y);

        switch (str) {

            case "up":
                if (!alreadyChecked.Contains(l)) { up = getCell(ground, ground.GetCellCenterWorld(temp)) != null; }
                else { up = false; }
                break;

            case "down":
                if (!alreadyChecked.Contains(l)) { down = getCell(ground, ground.GetCellCenterWorld(temp)) != null; }
                else { down = false; }
                break;

            case "left":
                if (!alreadyChecked.Contains(l)) { left = getCell(ground, ground.GetCellCenterWorld(temp)) != null; }
                else { left = false; }
                break;

            case "right":
                if (!alreadyChecked.Contains(l)) { right = getCell(ground, ground.GetCellCenterWorld(temp)) != null; }
                else { right = false; }
                break;

            default:
                Debug.Log("Error in Setbool method switch");
                break;
        }
    }

    private TileBase getCell(Tilemap tilemap, Vector3 cellWorldPos) {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }

    private List<Path> BuildPath(Path p) {
        List<Path> bestPath = new List<Path>();
        Path currentLoc = p;
        bestPath.Insert(0, currentLoc);
        while (currentLoc.parent != null) {
            currentLoc = currentLoc.parent;

            if (currentLoc.parent != null)
                bestPath.Insert(0, currentLoc);
        }
        return bestPath;
    }

    private IEnumerator MoveAi(List<Path> paths) {

        while (paths.Count > 0) {
            Vector3 temp = new Vector3(paths[0].pos.x, paths[0].pos.y, transform.position.z);
            temp = ground.GetCellCenterWorld(Vector3Int.RoundToInt(temp));

            Vector2 heading = temp - transform.position;

            if (heading == new Vector2(0f, 1f)) {
                anime.ResetTrigger("down");
                anime.ResetTrigger("left");
                anime.ResetTrigger("right");
                anime.SetTrigger("up");
            }
            else if (heading == new Vector2(0f, -1f)) {
                anime.ResetTrigger("left");
                anime.ResetTrigger("up");
                anime.ResetTrigger("right");
                anime.SetTrigger("down");
            }
            else if (heading == new Vector2(-1f, 0f)) {
                anime.ResetTrigger("down");
                anime.ResetTrigger("up");
                anime.ResetTrigger("right");
                anime.SetTrigger("left");
            }
            else if (heading == new Vector2(1f, 0f)) {
                anime.ResetTrigger("down");
                anime.ResetTrigger("left");
                anime.ResetTrigger("up");
                anime.SetTrigger("right");
            }
            else {
                Debug.Log("heading error");
            }


            float sqrRemainingDistance = (transform.position - temp).sqrMagnitude;

            while (sqrRemainingDistance > float.Epsilon) {
                
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), temp, moveTime * Time.deltaTime);



                //For 3D use this one
//                 Vector3 newPosition = Vector3.MoveTowards(transform.position, temp, moveTime * Time.deltaTime);
//                  transform.position = newPosition;



                sqrRemainingDistance = (new Vector2(transform.position.x,transform.position.y) - new Vector2(temp.x,temp.y)).sqrMagnitude;
                yield return null;
            }

            paths.RemoveAt(0);
            yield return null;
        }
        SetRunning(false);

        
    }

    public void SetRunning(bool x) {
        running = x;
    }

    public bool GetRunning() {
        return running;
    }

    public void HappyPath(Vector3 pos) {
        ResetLists();
        GetStartPositions(pos);
        IntiateAstar();

        bool found = false;
        int g = 0;
        while (openList.Count > 0 && found == false ) {
            if(g < 250f) {
                openList.Sort((x, y) => x.f.CompareTo(y.f));


                Path z = openList[0];

                SetBool(z.pos, 0, 1, "up");
                SetBool(z.pos, 0, -1, "down");
                SetBool(z.pos, 1, 0, "right");
                SetBool(z.pos, -1, 0, "left");


                BuildTiles(z);

                if (z.pos == targetPos) {
                    found = true;
                    List<Path> AiPath = BuildPath(z);
                    StartCoroutine(MoveAi(AiPath));

                }

                alreadyChecked.Add(z.pos);            //add our node to a list so it wont be checked again, reduced numbers of needed checks by 75% lol
                closedList.Add(z);                    //add our searched path to closed list
                openList.RemoveAt(0);                 //remove our searched path from open list
                g++;
            }
            else {
                Debug.Log("Can not navigate to waypoint");
            }
        }
    }
}




