using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/DestroyCheck")]
public class DestroyCheck : Decision
{

    public override bool Decide(StateController controller) {

        return LookAround(controller);

    }

    private bool LookAround(StateController controller) {

        if (controller.destroy == true) {

            return true;
        }
        else {

            return false;
        }



    }


}

