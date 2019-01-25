using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/DestroyAction")]
public class DestroyAction : Action
{

    public override void Act(StateController controller) {
        
        if(controller.toBeDestroyed == false) {
            Destroy(controller.gameObject, 3f);
        }      

    }



}
