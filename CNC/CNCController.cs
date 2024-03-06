using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CNCController : MonoBehaviour
{
   public enum State
   {
      Idle,
      Input,
      Running,
      Paused,
      Stopped,
      Finished
   }
   public State state;
   public static CNCController _instance;
   public static CNCController Instance { get { return _instance; } }
   [SerializeField]
   GameObject startlight, stoplight, pauselight;
   public GameObject input;
   [SerializeField]
   public GameObject outputPrefab;
   public double processTime;
   public double processTimeDone;
   private void Awake()
   {
      _instance = this;
      state = State.Idle;
      startlight.GetComponent<Light>().enabled = false;
      stoplight.GetComponent<Light>().enabled = false;
      pauselight.GetComponent<Light>().enabled = false;
   }
   public void StartButton()
   {
      Debug.Log("StartButton Pressed");
      if (state == State.Input)
      {
         changeState(State.Running);
      }
      else if(state == State.Running){
         StartCoroutine("showWarning", "CNC is already running");
      }
      else
      {
         StartCoroutine("showWarning", "Please put the object in the CNC machine");
      }
   }
   public void StopButton()
   {
      Debug.Log("StopButton Pressed");
      if (state == State.Running || state == State.Paused)
      {
         changeState(State.Stopped);
      }
      else
      {
         StartCoroutine("showWarning", "CNC is not running");
      }
   }
   public void PauseButton()
   {
      Debug.Log("PauseButton Pressed");
      if (state == State.Running)
      {
         changeState(State.Paused);
      }
      else if (state == State.Paused)
      {
         changeState(State.Running);
      }
      else
      {
         StartCoroutine("showWarning", "CNC is not running");
      }
   }
   public void changeState(State nextstate)
   {
      Debug.Log("State changed to " + nextstate);
      state = nextstate;
      startlight.GetComponent<Light>().enabled = false;
      stoplight.GetComponent<Light>().enabled = false;
      pauselight.GetComponent<Light>().enabled = false;
      if (state == State.Running)
      {
         startlight.GetComponent<Light>().enabled = true;
         startlight.GetComponent<Light>().intensity = 1;
      }
      else if (state == State.Stopped)
      {
         stoplight.GetComponent<Light>().enabled = true;
         stoplight.GetComponent<Light>().intensity = 1;
      }
      else if (state == State.Paused)
      {
         pauselight.GetComponent<Light>().enabled = true;
         pauselight.GetComponent<Light>().intensity = 1;
      }
      else if (state == State.Finished)
      {
         processTimeDone = 0;
         GameObject output = Instantiate(outputPrefab, new Vector3(0, 0, 0), Quaternion.identity);
         output.transform.SetParent(GameObject.Find("Output").transform);
         output.transform.localPosition = new Vector3(0, 0, 0);
         float outputMass = input.GetComponent<Rigidbody>().mass * 0.5f;
         output.transform.localScale = input.transform.localScale;
         Vector3 eulerAngle = new Vector3(90, -90, 0);
         Quaternion rotation = Quaternion.Euler(eulerAngle);
         output.transform.localRotation = rotation;
         output.transform.tag = "grabable";
         output.GetComponent<Rigidbody>().useGravity = true;
         output.GetComponent<Rigidbody>().isKinematic = false;
         output.GetComponent<Rigidbody>().velocity = Vector3.zero;
         output.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
         output.GetComponent<Rigidbody>().mass = outputMass;
         output.GetComponent<Rigidbody>().drag = input.GetComponent<Rigidbody>().drag;
         output.GetComponent<Rigidbody>().angularDrag = input.GetComponent<Rigidbody>().angularDrag;
         output.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
         output.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
         output.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
         output.GetComponent<Rigidbody>().solverIterations = 255;
         output.GetComponent<Rigidbody>().solverVelocityIterations = 255;
         output.GetComponent<Rigidbody>().maxAngularVelocity = 100;
         output.GetComponent<Rigidbody>().maxDepenetrationVelocity = 100;
         output.GetComponent<Rigidbody>().sleepThreshold = 0.005f;
         output.GetComponent<Rigidbody>().maxAngularVelocity = 100;
         output.GetComponent<Rigidbody>().maxDepenetrationVelocity = 100;
         output.GetComponent<Rigidbody>().sleepThreshold = 0.005f;
         output.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
         output.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
         output.GetComponent<Rigidbody>().solverIterations = 255;
         output.GetComponent<Rigidbody>().solverVelocityIterations = 255;
         output.GetComponent<Rigidbody>().maxAngularVelocity = 100;
         output.GetComponent<Rigidbody>().maxDepenetrationVelocity = 100;
         output.GetComponent<Rigidbody>().sleepThreshold = 0.005f;
         output.GetComponent<Rigidbody>().maxAngularVelocity = 100;
         output.GetComponent<Rigidbody>().maxDepenetrationVelocity = 100;
         output.GetComponent<Rigidbody>().sleepThreshold = 0.005f;
         output.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
         output.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
         output.GetComponent<Rigidbody>().solverIterations = 255;
         changeState(State.Idle);
         Destroy(input);
         input = null;
      }
   }
   IEnumerator showWarning(string warning)
   {
      GameObject warningTextObject = GameObject.Find("WarningText");
      TextMeshProUGUI warningText = warningTextObject.GetComponent<TextMeshProUGUI>();
      if (warningText.text != "")
      {
         yield return new WaitForSeconds(0.1f);
      }
      warningText.text = warning;
      yield return new WaitForSeconds(5);
      warningText.text = "";
   }
   public void Update()
   {
      if (state == State.Running)
      {
         processTimeDone += Time.deltaTime;
         if (processTimeDone >= processTime)
         {
            changeState(State.Finished);
         }
      }
   }
}
