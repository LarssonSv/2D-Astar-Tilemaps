using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/LookAround")]
public class LookAroundDecision : Decision {

	public override bool Decide (StateController controller) {
        
        return LookAround(controller);

    }

    private bool LookAround(StateController controller) {

        if(controller.waypoints.Count > 0) {

            return true;
        }
        else {

            return false;
        }



    }


}
