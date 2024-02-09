using UnityEngine;

public class OxigenScript : MonoBehaviour
{

    public GameObject leftLPipe;
    public GameObject linearPipe1;
    public GameObject linearPipe2;
    public GameObject rightLPipe;

    public Collider leftLPipeDestination;
    public Collider leftLPipeColliderAttached;
    public Collider rightLPipeDestination;
    public Collider rightLPipeColliderAttached;
    public Collider lineaPipe1ColliderAttached;
    public Collider lineaPipe2ColliderAttached;
    public Collider linearPipeDestination;
    Collider colliderDestionation;
    Collider colliderAttached;

    public GameObject robotLocation;

    GameObject selectedPipe;
    bool inRange = false;
    Quaternion correctRotation;
    Quaternion leftLPipeOriginalRotation;
    Quaternion linearPipe1OriginalRotation;
    Quaternion linearPipe2OriginalRotation;
    Quaternion rightLPipeOriginalRotation;
    int pipesAligned = 0;

    bool malfunctionOxigen = false;
    bool ifStatementCanRunOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        leftLPipeOriginalRotation = leftLPipe.transform.rotation;
        linearPipe1OriginalRotation = linearPipe1.transform.rotation;
        linearPipe2OriginalRotation = linearPipe2.transform.rotation;
        rightLPipeOriginalRotation = rightLPipe.transform.rotation;

        TasksScript.tasksScriptInstance.RobotFixOxigenPosition = robotLocation.transform.position;
        TasksScript.tasksScriptInstance.RobotFixOxigenRotation = robotLocation.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //reseting the bool "ifStatementCanRunOnce" that sets different roations for the pipes when they malfunction
        if (TasksScript.tasksScriptInstance.resetRobots)
        {
            ifStatementCanRunOnce = true;
        }

        //pipes malfuction rotation change
        if (TasksScript.tasksScriptInstance.oxigenTaskActive && ifStatementCanRunOnce)
        {
            ifStatementCanRunOnce = false;
            malfunctionOxigen = true;

            leftLPipe.transform.localRotation = Quaternion.Euler(0, 90, -270);
            linearPipe1.transform.localRotation = Quaternion.Euler(0, 90, -180);
            linearPipe2.transform.localRotation = Quaternion.Euler(0, 90, 180);
            rightLPipe.transform.localRotation = Quaternion.Euler(0, 90, -90);
        }

        //if robot dispatched to fix, fix the malfuction
        if (malfunctionOxigen && TasksScript.tasksScriptInstance.robotFixOxigen)
        {
            leftLPipe.transform.rotation = leftLPipeOriginalRotation;
            linearPipe1.transform.rotation = linearPipe1OriginalRotation;
            linearPipe2.transform.rotation = linearPipe2OriginalRotation;
            rightLPipe.transform.rotation = rightLPipeOriginalRotation;

            TasksScript.tasksScriptInstance.taskText3.text = "Fix Oxigen Room - Completed";
            malfunctionOxigen = false;
            TasksScript.tasksScriptInstance.oxigenTaskActive = false;
            pipesAligned = 0;
            TasksScript.tasksScriptInstance.robotFixOxigen = false;
        }

        //if player is in range he can interact by pressing 1 2 3 4 and Q or E to rotate pipes
        if (inRange && malfunctionOxigen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedPipe = leftLPipe;
                correctRotation = leftLPipeOriginalRotation;

                colliderAttached = leftLPipeColliderAttached;
                colliderDestionation = leftLPipeDestination;

                if (selectedPipe.transform.rotation == correctRotation)
                {
                    Debug.Log("This pipe is correctly alinged and you can t move it anymore\nSelect another pipe'");
                    selectedPipe = null;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedPipe = linearPipe1;
                correctRotation = linearPipe1OriginalRotation;

                colliderAttached = lineaPipe1ColliderAttached;
                colliderDestionation = linearPipeDestination;

                if (selectedPipe.transform.rotation == correctRotation)
                {
                    Debug.Log("This pipe is correctly alinged and you can t move it anymore\nSelect another pipe'");
                    selectedPipe = null;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selectedPipe = linearPipe2;
                correctRotation = linearPipe2OriginalRotation;

                colliderAttached = lineaPipe2ColliderAttached;
                colliderDestionation = linearPipeDestination;

                if (selectedPipe.transform.rotation == correctRotation)
                {
                    Debug.Log("This pipe is correctly alinged and you can t move it anymore\nSelect another pipe'");
                    selectedPipe = null;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                selectedPipe = rightLPipe;
                correctRotation = rightLPipeOriginalRotation;

                colliderAttached = rightLPipeColliderAttached;
                colliderDestionation = rightLPipeDestination;

                if (selectedPipe.transform.rotation == correctRotation)
                {
                    Debug.Log("This pipe is correctly alinged and you can t move it anymore\nSelect another pipe'");
                    selectedPipe = null;
                }
            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                selectedPipe.transform.rotation *= Quaternion.Euler(0, 0, 90);
            }

            if (Input.GetKeyUp(KeyCode.E) && colliderAttached.bounds.Intersects(colliderDestionation.bounds))
            {
                Debug.Log("You aligned the pipe. Chose another");
                selectedPipe = null;
                pipesAligned++;

                colliderAttached = null;
                colliderDestionation = null;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                selectedPipe.transform.rotation *= Quaternion.Euler(0, 0, -90);
            }

            if (Input.GetKeyUp(KeyCode.Q) && colliderAttached.bounds.Intersects(colliderDestionation.bounds))
            {
                Debug.Log("You aligned the pipe. Chose another");
                selectedPipe = null;
                pipesAligned++;

                colliderAttached = null;
                colliderDestionation = null;
            }

            if (pipesAligned == 4)
            {
                TasksScript.tasksScriptInstance.taskText3.text = "Fix Oxigen Room - Completed";
                malfunctionOxigen = false;
                TasksScript.tasksScriptInstance.oxigenTaskActive = false;
                pipesAligned = 0;
                TasksScript.tasksScriptInstance.staminaSlider.value -= 10f;
            }

            if (TasksScript.tasksScriptInstance.sleep)
            {
                pipesAligned = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        selectedPipe = null;
    }
}
