using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Unity;
/// <summary>
/// mainly use motion capture data to control the character
/// 21 - left thumb
/// 22 - right thumb
/// </summary>

namespace MoC
{
    public class MoCManager : MonoBehaviour
    {
        public static MoCManager instance;
        Queue leftX = new Queue();
        Queue leftY = new Queue();
        Queue rightX = new Queue();
        Queue rightY = new Queue();
        [SerializeField]
        private float lowPassFilterFactor = 0.5f;
        private float preleftX = 0;
        private float preleftY = 0;
        private float prerightX = 0;
        private float prerightY = 0;
        private Queue lastLeftX = new Queue();
        private Queue lastLeftY = new Queue();
        private Queue lastRightX = new Queue();
        private Queue lastRightY = new Queue();
        [SerializeField]
        GameObject mainPlayer;
        public void Start()
        {
            instance = this;
        }
        public void addData(float leftx, float lefty, float leftVisible, float rightx, float righty, float rightVisible)
        {
            if (leftVisible > 0.7)
            {
                preleftX = lowPassFilterFactor * leftx + (1 - lowPassFilterFactor) * preleftX;
                preleftY = lowPassFilterFactor * lefty + (1 - lowPassFilterFactor) * preleftY;
                leftX.Enqueue(preleftX);
                leftY.Enqueue(preleftY);
                if (leftX.Count > 50)
                {
                    lastLeftX.Enqueue(leftX.Dequeue());
                    lastLeftY.Enqueue(leftY.Dequeue());
                }
                if (lastLeftX.Count > 50)
                {
                    lastLeftX.Dequeue();
                    lastLeftY.Dequeue();
                }
            }
            if (rightVisible > 0.7)
            {
                prerightX = lowPassFilterFactor * rightx + (1 - lowPassFilterFactor) * prerightX;
                prerightY = lowPassFilterFactor * righty + (1 - lowPassFilterFactor) * prerightY;
                rightX.Enqueue(prerightX);
                rightY.Enqueue(prerightY);
                if (rightX.Count > 50)
                {
                    lastRightX.Enqueue(rightX.Dequeue());
                    lastRightY.Enqueue(rightY.Dequeue());
                }
                if (lastRightX.Count > 50)
                {
                    lastRightX.Dequeue();
                    lastRightY.Dequeue();
                }
            }
        }
        public void Update()
        {
            motionDetect();
        }
        private void motionDetect()
        {
            if (lastLeftX.Count == 50 && lastRightX.Count == 50)
            {
                float leftXAvg = calAvg(leftX);
                float leftYAvg = calAvg(leftY);
                float rightXAvg = calAvg(rightX);
                float rightYAvg = calAvg(rightY);
                float lastLeftXAvg = calAvg(lastLeftX);
                float lastLeftYAvg = calAvg(lastLeftY);
                float lastRightXAvg = calAvg(lastRightX);
                float lastRightYAvg = calAvg(lastRightY);
                if (leftXAvg - lastLeftXAvg < -0.2) //from left to right, player try to grab an object
                {
                    Debug.Log("try to grab in MoC");
                    mainPlayer.GetComponent<SceneInteraction>().grabObject();
                }
                if(leftXAvg-lastLeftXAvg>0.2) //from right to left, player try to throw an object
                {
                    Debug.Log("try to throw in MoC");
                    mainPlayer.GetComponent<SceneInteraction>().throwObject();
                }
            }
        }
        private float calAvg(IEnumerable collection)
        {
            float sum = 0;
            int count = 0;
            foreach (float i in collection)
            {
                sum += i;
                count++;
            }
            return sum / count;
        }
    }

}
