using UnityEngine;
using TMPro;

public class RobotSystem : MonoBehaviour
{
    public GameObject robot1;
    public GameObject robot2;
    public GameObject robotUpgradePosition;
    public GameObject ghostRobot;

    public Material upgradedMaterial;
    Transform headPart;
    Transform torsoPart;

    Vector3 robot1OGPosition;
    Vector3 robot2OGPosition;
    Quaternion robot1OGRotation;
    Quaternion robot2OGRotation;

    public GameObject pipe1;
    public GameObject pipe2;
    public GameObject pipe1Up;
    public GameObject pipe1Down;
    public GameObject pipe2Up;
    public GameObject pipe2Down;

    GameObject selectedRobot;
    GameObject selectedPipe;

    public TextMeshProUGUI monitorDisplay;

    bool ifStatementCanRunOnce = true;

    bool inRange = false;
    bool interacted = false;
    bool robotChosen = false;
    bool actionChosen = false;
    bool robotDispatched = false;

    bool displayOptions = false;
    bool canChoose = true;

    bool robot1Available = true;
    bool robot2Available = true;

    int actionNumber = 0;
    float time = 0f;
    float timerToDispatch = 0;
    bool timerStart = false;

    // Start is called before the first frame update
    void Start()
    {
        robot1OGPosition = robot1.transform.position;
        robot1OGRotation = robot1.transform.rotation;
        robot2OGPosition = robot2.transform.position;
        robot2OGRotation = robot2.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (TasksScript.tasksScriptInstance.resetRobots)
        {
            robot1Available = true;
            robot2Available = true;
            
            if (robot1Available)
            {
                robot1.transform.position = robot1OGPosition;
                robot1.transform.rotation = robot1OGRotation;
            }

            if (robot2Available)
            {
                robot2.transform.position = robot2OGPosition;
                robot2.transform.rotation = robot2OGRotation;
            }

            if (TasksScript.tasksScriptInstance.robot1Upgrading == true)
            {
                robot1Available = false;

                robot1.transform.SetPositionAndRotation(robotUpgradePosition.transform.position, robotUpgradePosition.transform.rotation);
            }

            if (TasksScript.tasksScriptInstance.robot2Upgrading == true)
            {
                robot2Available = false;

                robot2.transform.SetPositionAndRotation(robotUpgradePosition.transform.position, robotUpgradePosition.transform.rotation);
            }

            ifStatementCanRunOnce = false;
            TasksScript.tasksScriptInstance.resetRobots = false;

            TasksScript.tasksScriptInstance.robotFixElectrical = false;
            TasksScript.tasksScriptInstance.robotFixOxigen = false;
            TasksScript.tasksScriptInstance.robotInspectOxigen = false;
            TasksScript.tasksScriptInstance.robotInspectElectrical = false;
        }

        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !interacted)
            {
                monitorDisplay.text = "Press 1 or 2 to select one of the robots";
                interacted = true;
            }

            if (interacted && !robotChosen)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && robot1Available)
                {
                    selectedRobot = robot1;
                    selectedPipe = pipe1;

                    monitorDisplay.text = "You selected the Robot1. Press Enter to confirm your choice...";
                }
                else if (Input.GetKeyDown(KeyCode.Alpha1) && !robot1Available)
                {
                    monitorDisplay.text = "The Robot you selected is currently unavailable...\nPress 1 or 2 to select one of the robots";
                    selectedRobot = ghostRobot;
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) && robot2Available)
                {
                    selectedRobot = robot2;
                    selectedPipe = pipe2;

                    monitorDisplay.text = "You selected the Robot2. Press Enter to confirm your choice...";
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2) && !robot2Available)
                {
                    monitorDisplay.text = "The Robot you selected is currently unavailable...\nPress 1 or 2 to select one of the robots";
                    selectedRobot = ghostRobot;
                }

                if (Input.GetKeyDown(KeyCode.Return) && selectedRobot == robot1 || displayOptions && selectedRobot == robot1)
                {
                    monitorDisplay.text = "Selected robot: Robot1 \nChoose the action:\n 1 - Fix Electrical\n 2 - Fix Oxigen\n 3 - Inspect Electrical\n 4 - Inspect Oxigen\n 5 - Self-Upgrade";
                    robotChosen = true;
                    canChoose = true;
                }

                if (Input.GetKeyDown(KeyCode.Return) && selectedRobot == robot2 || displayOptions && selectedRobot == robot2)
                {
                    monitorDisplay.text = "Selected robot: Robot2 \nChoose the action:\n 1 - Fix Electrical\n 2 - Fix Oxigen\n 3 - Inspect Electrical\n 4 - Inspect Oxigen\n 5 - Self-Upgrade";
                    robotChosen = true;
                    canChoose = true;
                }
            }

            if (robotChosen && !actionChosen)
            {
                if (Input.GetKeyUp(KeyCode.Alpha1) && canChoose && !TasksScript.tasksScriptInstance.robotFixElectrical)
                {
                    monitorDisplay.text = "The Robot will be dispatched to fix Electrical.\nCost: 15 Energy.\nPress Enter to confirm your choice\nPress Backspace to go back";
                    actionNumber = 1;

                    canChoose = false;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha1) && canChoose && TasksScript.tasksScriptInstance.robotFixElectrical)
                {
                    monitorDisplay.text = "A Robot is currently working on thisk task.\nPress Backspace to select another action";
                    canChoose = false;
                }

                if (Input.GetKeyUp(KeyCode.Alpha2) && canChoose && !TasksScript.tasksScriptInstance.robotFixOxigen)
                {
                    monitorDisplay.text = "The Robot will be dispatched to fix Oxigen.\nCost: 15 Energy.\nPress Enter to confirm your choice\nPress Backspace to go back";
                    actionNumber = 2;

                    canChoose = false;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha2) && canChoose && TasksScript.tasksScriptInstance.robotFixOxigen)
                {
                    monitorDisplay.text = "A Robot is currently working on thisk task.\nPress Backspace to select another action";
                    canChoose = false;
                }

                if (Input.GetKeyUp(KeyCode.Alpha3) && canChoose && !TasksScript.tasksScriptInstance.robotInspectElectrical)
                {
                    monitorDisplay.text = "The Robot will be dispatched to inspect Electrical.\nCost: 10 Energy.\nPress Enter to confirm your choice\nPress Backspace to go back";
                    actionNumber = 3;

                    canChoose = false;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha3) && canChoose && TasksScript.tasksScriptInstance.robotInspectElectrical)
                {
                    monitorDisplay.text = "A Robot is currently working on thisk task.\nPress Backspace to select another action";
                    canChoose = false;
                }

                if (Input.GetKeyUp(KeyCode.Alpha4) && canChoose && !TasksScript.tasksScriptInstance.robotInspectOxigen)
                {
                    monitorDisplay.text = "The Robot will be dispatched to inspect Oxigen\nCost 10 Energy.\nPress Enter to confirm your choice\nPress Backspace to go back";
                    actionNumber = 4;

                    canChoose = false;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha4) && canChoose && TasksScript.tasksScriptInstance.robotInspectOxigen)
                {
                    monitorDisplay.text = "A Robot is currently working on thisk task.\nPress Backspace to select another action";
                    canChoose = false;
                }

                if (Input.GetKeyUp(KeyCode.Alpha5) && canChoose && !TasksScript.tasksScriptInstance.robot1Upgrading && !TasksScript.tasksScriptInstance.robot2Upgrading)
                {
                    monitorDisplay.text = "The upgrade will take 3 days\nCost:30 Energy/Day.\nPress Enter to confirm your choice\nPress Backspace to go back";
                    actionNumber = 5;

                    canChoose = false;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha5) && canChoose && TasksScript.tasksScriptInstance.robot1Upgrading || Input.GetKeyUp(KeyCode.Alpha5) && canChoose && TasksScript.tasksScriptInstance.robot2Upgrading)
                {
                    monitorDisplay.text = "A robot is already being upgraded.\nPress Backspace to select another action";
                    canChoose = false;
                }

                if (Input.GetKeyDown(KeyCode.Return) && actionNumber != 0)
                {
                    timerStart = true;
                    monitorDisplay.text = "Robot will be dispatched soon...";
                }

                if (timerStart)
                {
                    timerToDispatch += Time.deltaTime;
                    PlayerMovement.playerMovementInstance.speed = 0f;

                    if (selectedPipe == pipe1)
                    {
                        pipe1.transform.position = Vector3.Lerp(pipe1.transform.position, pipe1Down.transform.position, 1f * Time.deltaTime);
                    }
                    
                    if (selectedPipe == pipe2)
                    {
                        pipe2.transform.position = Vector3.Lerp(pipe2.transform.position, pipe2Down.transform.position, 1f * Time.deltaTime);
                    }

                    if (timerToDispatch > 4f)
                    {
                        actionChosen = true;
                        timerStart = false;
                        timerToDispatch = 0f;
                    }
                }


                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    displayOptions = true;
                    robotChosen = false;
                    actionNumber = 0;
                }
            }

            if (actionChosen && !robotDispatched)
            {
                if (actionNumber == 1)
                {
                    monitorDisplay.text = "Robot has been dispatched and is currently fixing Electrical";
                    TasksScript.tasksScriptInstance.robotFixElectrical = true;
                    selectedRobot.transform.position = TasksScript.tasksScriptInstance.RobotFixElectricalPosition;
                    selectedRobot.transform.rotation = TasksScript.tasksScriptInstance.RobotFixElectricalRotation;
                    TasksScript.tasksScriptInstance.electricitySlider.value -= 15f;
                }
                else if (actionNumber == 2)
                {
                    monitorDisplay.text = "Robot has been dispatched and is currently fixing Oxigen";
                    TasksScript.tasksScriptInstance.robotFixOxigen = true;
                    selectedRobot.transform.position = TasksScript.tasksScriptInstance.RobotFixOxigenPosition;
                    selectedRobot.transform.rotation = TasksScript.tasksScriptInstance.RobotFixOxigenRotation;
                    TasksScript.tasksScriptInstance.electricitySlider.value -= 15f;
                }
                else if (actionNumber == 3)
                {
                    monitorDisplay.text = "Robot has been dispatched and is currently inspecting Electrical";
                    TasksScript.tasksScriptInstance.robotInspectElectrical = true;
                    selectedRobot.transform.position = TasksScript.tasksScriptInstance.RobotInspectElectricalPosition;
                    selectedRobot.transform.rotation = TasksScript.tasksScriptInstance.RobotInspectElectricalRotation;
                    TasksScript.tasksScriptInstance.electricitySlider.value -= 10f;
                }
                else if (actionNumber == 4)
                {
                    monitorDisplay.text = "Robot has been dispatched and is currently inspecting Oxigen";
                    TasksScript.tasksScriptInstance.robotInspectOxigen = true;
                    selectedRobot.transform.position = TasksScript.tasksScriptInstance.RobotInspectOxigenPosition;
                    selectedRobot.transform.rotation = TasksScript.tasksScriptInstance.RobotInspectOxigenRotation;
                    TasksScript.tasksScriptInstance.electricitySlider.value -= 10f;
                }
                else if (actionNumber == 5)
                {
                    monitorDisplay.text = "Robot has been dispatched for the Self-Upgrade.\nIt will return after 3 days";
                    if (selectedRobot == robot1)
                    {
                        TasksScript.tasksScriptInstance.robot1Upgrading = true;
                    }
                    
                    if (selectedRobot == robot2)
                    {
                        TasksScript.tasksScriptInstance.robot2Upgrading = true;
                    }

                    selectedRobot.transform.SetPositionAndRotation(robotUpgradePosition.transform.position, robotUpgradePosition.transform.rotation);

                    headPart = selectedRobot.transform.GetChild(2);
                    headPart.GetComponent<Renderer>().material = upgradedMaterial;
                    torsoPart = selectedRobot.transform.GetChild(4);
                    torsoPart.GetComponent<Renderer>().material = upgradedMaterial;
                }

                robotDispatched = true;

                if (selectedRobot == robot1)
                {
                    robot1Available = false;
                }
                else if (selectedRobot == robot2)
                {
                    robot2Available = false;
                }
            }

            if (robotDispatched)
            {
                time += Time.deltaTime;
                if (selectedPipe == pipe1)
                {
                    pipe1.transform.position = Vector3.Lerp(pipe1.transform.position, pipe1Up.transform.position, 2.5f * Time.deltaTime);
                }

                if (selectedPipe == pipe2)
                {
                    pipe2.transform.position = Vector3.Lerp(pipe2.transform.position, pipe2Up.transform.position, 2.5f * Time.deltaTime);
                }

                actionNumber = 0;
                if (time > 2f)
                {
                    interacted = false;
                    robotChosen = false;
                    actionChosen = false;
                    robotDispatched = false;
                    displayOptions = false;
                    selectedRobot = ghostRobot;

                    actionNumber = 0;
                    time = 0;
                    monitorDisplay.text = "Press E to interact with the\nRobot Managment Pannel";
                    PlayerMovement.playerMovementInstance.speed = 6f;
                }
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

        monitorDisplay.text = "Press E to interact with the\nRobot Managment Pannel";

        interacted = false;
        robotChosen = false;
        actionChosen = false;
        robotDispatched = false;
        displayOptions = false;
        selectedRobot = ghostRobot;
        actionNumber = 0;
        time = 0;
    }
}