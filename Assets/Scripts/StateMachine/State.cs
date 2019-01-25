using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateMachine/State")]
public class State : ScriptableObject {

    public Action[] actions;
    public Transition[] transitions;
    public Color sceneGizmoColor = Color.red;

    public void UpdateState(StateController controller) {

        CheckTranstions(controller);
        DoActions(controller);
        

        controller.gizmoColor = sceneGizmoColor;
    }

    private void DoActions(StateController controller) {

        foreach (Action x in actions) {
            x.Act(controller);
        }


    }

    private void CheckTranstions(StateController controller) {

        foreach (Transition x in transitions) {
            bool desisionSucceeded = x.decision.Decide(controller);


            if (desisionSucceeded) {
                controller.TransitionToState(x.trueState);
            }
            else {

                controller.TransitionToState(x.falseState);

            }

        }

    }



}
