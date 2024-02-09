using UnityEngine;

public class ElectricalScript : MonoBehaviour
{
    public GameObject leftSwitch;
    public GameObject rightSwitch;

    public Collider leftSwitchRedCollider;
    public Collider redDestination;
    public Collider rightSwitchBlueCollider;
    public Collider blueDestination;

    public GameObject robotLocation;


    public TasksScript tasks;

    bool inRange = false;
    bool leftSwitchSelected = false;
    bool rightSwitchSelected = false;

    bool malfuntionElectric = false;
    bool ifStatementCanRunOnce = true;

    bool leftSwitchFixed = false;
    bool rightSwitchFixed = false;

    Quaternion leftSwitchFixedPosition;
    Quaternion rightSwitchFixedPosition;

    // Start is called before the first frame update
    void Start()
    {
        leftSwitchFixedPosition = leftSwitch.transform.rotation;
        rightSwitchFixedPosition = rightSwitch.transform.rotation;

        TasksScript.tasksScriptInstance.RobotFixElectricalPosition = robotLocation.transform.position;
        TasksScript.tasksScriptInstance.RobotFixElectricalRotation = robotLocation.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //reseting the bool "ifStatementCanRunOnce" that sets different rotations for the switches when they malfunction
        if (TasksScript.tasksScriptInstance.resetRobots)
        {
            ifStatementCanRunOnce = true;
        }

        //setting diferent initial rotations for the switches
        if (TasksScript.tasksScriptInstance.electricalTaskActive && ifStatementCanRunOnce)
        {
            malfuntionElectric = true;
            ifStatementCanRunOnce = false;

            leftSwitch.transform.rotation = Quaternion.Euler(0, 0, Random.Range(40, 360));
            rightSwitch.transform.rotation = Quaternion.Euler(0, 0, Random.Range(40, 360));
        }

        //if robot dispatched to fix, fix the malfuction
        if (malfuntionElectric && TasksScript.tasksScriptInstance.robotFixElectrical)
        {
            leftSwitch.transform.rotation = leftSwitchFixedPosition;
            rightSwitch.transform.rotation = rightSwitchFixedPosition;

            tasks.taskText2.text = "Fix Energy Room - Completed";

            //reseting so that it doesnt keep checking as true every frame and drains the stamina in an instant
            leftSwitchFixed = false;
            rightSwitchFixed = false;

            //resetting values for next time the electrical quest gets activated
            malfuntionElectric = false;
            //ifStatementCanRunOnce = true;
            TasksScript.tasksScriptInstance.electricalTaskActive = false;

            TasksScript.tasksScriptInstance.robotFixElectrical = false;
        }

        //makes the switches to keep rotating as long as the Electrical is not fixed
        if (malfuntionElectric)
        {
            if (!leftSwitchFixed)
            {
                leftSwitch.transform.rotation *= Quaternion.Euler(0f, 0f, .5f);
            }

            if (!rightSwitchFixed)
            {
                rightSwitch.transform.rotation *= Quaternion.Euler(0f, 0f, .5f);
            }
        }

        //if player is in range he can interact by pressing 1 2 and E to align the switch
        if (inRange)
        {
            TasksScript.tasksScriptInstance.eToInteract.text = "Press 1 or 2 to select the switch. Press E to align the switch.";

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                leftSwitchSelected = true;
                rightSwitchSelected = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                leftSwitchSelected = false;
                rightSwitchSelected = true;
            }

            if (leftSwitchSelected && Input.GetKeyDown(KeyCode.E) && leftSwitchRedCollider.bounds.Intersects(redDestination.bounds))
            {
                leftSwitchFixed = true;
                leftSwitch.transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            if (rightSwitchSelected && Input.GetKeyDown(KeyCode.E) && rightSwitchBlueCollider.bounds.Intersects(blueDestination.bounds))
            {
                rightSwitchFixed = true;
                rightSwitch.transform.rotation = Quaternion.Euler(0, 0, 180);
            }


            if (leftSwitchFixed && rightSwitchFixed)
            {
                tasks.taskText2.text = "Fix Energy Room - Completed";
                TasksScript.tasksScriptInstance.staminaSlider.value -= 10f;

                //reseting so that it doesnt keep checking as true every frame and drains the stamina in an instant
                leftSwitchFixed = false;
                rightSwitchFixed = false;

                //resetting values for next time the electrical quest gets activated
                malfuntionElectric = false;
                ifStatementCanRunOnce = true;
                TasksScript.tasksScriptInstance.electricalTaskActive = false;
            }
        }

        if (TasksScript.tasksScriptInstance.sleep)
        {
            leftSwitchFixed = false;
            rightSwitchFixed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        TasksScript.tasksScriptInstance.eToInteract.fontSize = 20;
    }


    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        TasksScript.tasksScriptInstance.eToInteract.fontSize = 0;
        TasksScript.tasksScriptInstance.eToInteract.text = "Press E to interact";
        leftSwitchSelected = false;
        rightSwitchSelected = false;
    }
}