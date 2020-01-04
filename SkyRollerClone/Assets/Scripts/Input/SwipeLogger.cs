using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyRollerClone.Input
{
    public class SwipeLogger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            SwipeDetector.OnSwipe += SwipeLog;
        }

        private void SwipeLog(SwipeData swipeData)
        {
            Debug.Log("Swipe " + swipeData.direction + " dist = " + (swipeData.end.x - swipeData.start.x));
        }


    }
}