using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/WaitingForAction")]
public class WaitingForAction : Action
{

    public override void Act(StateController controller) {

        controller.waypoints.Add(GameManager.GM.exits[Random.Range(0, 4)]);

    }



}
