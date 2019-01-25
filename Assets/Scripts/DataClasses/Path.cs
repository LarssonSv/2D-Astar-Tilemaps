using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : object {

    public float g;         // Steps from A to this
    public float h;         // Steps from this to B
    public Path parent;     // Parent node in the path
    public Vector2Int pos;  //posInCell

    public Path(float g, float h, Path parent, Vector2Int pos) {
        this.g = g;
        this.h = h;
        this.parent = parent;
        this.pos = pos;
    }

    public float f // Total score for this
    {
        get {
            return g + h;
        }
    }
}
