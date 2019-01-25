using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {

    public State currentState;
    public State stayInState;
    public bool active = true;
    public Vector3 currentWaypoint;

    public bool destroy = false;
    public bool toBeDestroyed = false;

    [HideInInspector] public List<WaypointData> waypoints;
    [HideInInspector]public float stateTimeElapsed;
    [HideInInspector]public AStar astar;
    [HideInInspector]public Color gizmoColor;

   


    private void Start() {
        astar = GetComponent<AStar>();
        waypoints = new List<WaypointData>(GameManager.GM.exits);
    }

    public void SetupWaypoint() {

        
    }

    public void SetActive(bool x) {
        active = x;
    }


    private void Update() {

        if (active && astar.running == false) {
            currentState.UpdateState(this);
        }


    }

    public void TransitionToState (State nextState) {

        if(nextState != stayInState) {
            currentState = nextState;
        }

    }

    public bool CooldownCheck (float duration) {

        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState() {
        stateTimeElapsed = 0;
    }

    private void OnDrawGizmos() {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), 0.1f);
    }





}
