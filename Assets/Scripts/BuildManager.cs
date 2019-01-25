using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildManager : MonoBehaviour
{

    public GameObject houseTest;


    public static BuildManager BUILDER;
    public Tilemap groundTile;
    public Tilemap buildTile;

    public TileBase test;

    private void Awake() {
        BUILDER = this;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            SingelSlotSolid(null, GetMouseTile());
            //MultipleSlotsSolid(test, GetMouseTile(), 3, 2); //needs some checking
            BuildSite(GetMouseTile(), houseTest);


        }
    }

    private void BuildSite(Vector3Int pos, GameObject prefabData) {

        BuildData data = prefabData.GetComponent<BuildData>();


        Vector3Int prefabPos = pos; //Hack
        pos.x -= 1; //Hack
        pos.y -= 1; //Hack

        int sumCheck = 0;
        Vector3Int currentSearch = pos;
        List<Vector3Int> toAdd = new List<Vector3Int>();

        for (int i = 0; i < data.y; i++) {

            currentSearch.y += 1;

            for (int j = 0; j < data.x; j++) {
                currentSearch.x += 1;


                if (getCell(buildTile, buildTile.GetCellCenterLocal(currentSearch)) != null) {

                    toAdd.Add(currentSearch);
                    sumCheck++;

                    if (sumCheck == data.sum) {

                        foreach (Vector3Int p in toAdd) {

                            buildTile.SetTile(p, null);

                        }

                        Instantiate(prefabData, buildTile.CellToWorld(prefabPos), prefabData.transform.rotation);




                    }
                }

            }
            currentSearch.x = pos.x;
        }



    }


    public Vector3Int GetMouseTile() {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider) {

            Vector3Int temp = buildTile.WorldToCell(hit.point);
            return temp;

        }

        Debug.Log("Ray did not hit a collider of a tilemap");
        return new Vector3Int(99, 99, 99);
    }

    private void SingelSlotSolid(TileBase x, Vector3Int pos) {

        buildTile.SetTile(pos, x);

    }

    private void MultipleSlotsSolid(TileBase x, Vector3Int pos, int areaX,  int areaY) {

        pos.x -= 1; //hack mouse
        pos.y -= 1; //hack mouse

        int sumCheck = 0;
        int sum = areaX * areaY;

        Vector3Int currentSearch = pos;
        List<Vector3Int> toAdd = new List<Vector3Int>();

        for (int i = 0; i < areaY; i++) {

            currentSearch.y += 1; //Hoppa tile

            for (int j = 0; j < areaX; j++) {
                currentSearch.x += 1; //hoppa tile


                if (getCell(buildTile, buildTile.GetCellCenterLocal(currentSearch)) != null) {

                    toAdd.Add(currentSearch);
                    sumCheck++;

                    if (sumCheck == sum) {

                        foreach (Vector3Int p in toAdd) {

                            buildTile.SetTile(p, test);

                        }

                        //Do stuff when build completed




                    }
                }

            }
            currentSearch.x = pos.x;
        }


        //make into bool check












    }

    private TileBase getCell(Tilemap tilemap, Vector3 cellWorldPos) {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }









}
