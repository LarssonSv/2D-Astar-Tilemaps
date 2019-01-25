using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Patrol")]
public class PatrolAction : Action
{

    public override void Act(StateController controller) {

        if (controller.astar.GetRunning() == false) {

            if (controller.waypoints.Count > 0) {
                Patrol(controller, controller.waypoints[0].pos.position);
                controller.waypoints.RemoveAt(0);
            }

            else {
                Debug.Log("Out of waypoints");
                controller.destroy = true;
            }
        }
    }

    private void Patrol(StateController controller, Vector3 pos) {

        controller.currentWaypoint = pos;
        controller.astar.SetRunning(true);
        controller.astar.HappyPath(pos);

    }


}
