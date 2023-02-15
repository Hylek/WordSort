using DylansDen.Scripts.Utils;
using UnityEngine;

namespace Tools
{
    [System.Serializable]
    public class UIAnimatorData
    {
        public UIAnimatorMode mode;
        public EasingMethods.Ease easeType;
        public float startDelay = 0;
        public float endDelay = 0;
        public float startValue = 0;
        public float endValue = 0;
        public float speedValue = 0;
        public Vector3 startPosition = Vector3.zero;
        public Vector3 endPosition = Vector3.zero;
    }
}
