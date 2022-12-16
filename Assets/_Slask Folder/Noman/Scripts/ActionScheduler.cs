using System.Collections;
using System.Collections.Generic;
using Action;
using UnityEngine;

namespace ActionHandler
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        /// <summary>
        /// this checks what action is being done and that it continues doing it unless another action is being called, and if it does, then the current action gets cancelled.
        /// </summary>
        /// <param name="action"></param>
        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }

            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
