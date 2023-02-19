using System;

namespace Tools
{
    public interface ITransitionController
    {
        public Action<bool> TransitionComplete { get; set; }
        public void StartTransition(UIAnimatorTrigger trigger);
        public void StartTransitionManually(UIAnimatorTrigger trigger);
        public bool IsInManualMode();
    }
}
