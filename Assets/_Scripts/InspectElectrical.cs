using UnityEngine;

public class InspectElectrical : MonoBehaviour
{
    bool inRange = false;

    public GameObject firstButton;
    public GameObject secondButton;
    public GameObject thirdButton;
    public GameObject firstButtonPushed;
    public GameObject secondButtonPushed;
    public GameObject thirdButtonPushed;

    public GameObject robotLocation;

    GameObject selectedButton;
    GameObject selectedButtonPushed;
    public GameObject noButtonSelected;

    Vector3 firstPosSaved;
    Vector3 secondPosSaved;
    Vector3 thirdPosSaved;

    bool ifStatementCanRunOnce = true;

    int buttonsPushed = 0;
    // Start is called before the first frame update
    void Start()
    {
        selectedButton = noButtonSelected;

        firstPosSaved = firstButton.transform.position;
        secondPosSaved = secondButton.transform.position;
        thirdPosSaved = thirdButton.transform.position;

        firstButton.transform.position = firstButtonPushed.transform.position;
        secondButton.transform.position = secondButtonPushed.transform.position;
        thirdButton.transform.position = thirdButtonPushed.transform.position;

        TasksScript.tasksScriptInstance.RobotInspectElectricalPosition = robotLocation.transform.position;
        TasksScript.tasksScriptInstance.RobotInspectElectricalRotation = robotLocation.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (ifStatementCanRunOnce && TasksScript.tasksScriptInstance.sleep)
        {
            ifStatementCanRunOnce = false;

            firstButton.transform.position = firstPosSaved;
            secondButton.transform.position = secondPosSaved;
            thirdButton.transform.position = thirdPosSaved;
        }

        if (TasksScript.tasksScriptInstance.robotInspectElectrical)
        {
            firstButton.transform.position = firstButtonPushed.transform.position;
            secondButton.transform.position = secondButtonPushed.transform.position;
            thirdButton.transform.position = thirdButtonPushed.transform.position;
            

            TasksScript.tasksScriptInstance.electricalInspectionDone = true;
        }

        if (TasksScript.tasksScriptInstance.resetInspections)
        {
            ifStatementCanRunOnce = true;
        }

        //if close to object we can interact by pressing 1 2 or 3 and E to push the buttons down
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedButton = firstButton;
                selectedButtonPushed = firstButtonPushed;

                if (Vector3.Distance(selectedButton.transform.position, selectedButtonPushed.transform.position) < 0.002f)
                {                
                    selectedButton = noButtonSelected;
                    selectedButtonPushed = noButtonSelected;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedButton = secondButton;
                selectedButtonPushed = secondButtonPushed;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selectedButton = thirdButton;
                selectedButtonPushed = thirdButtonPushed;
            }

            if (Input.GetKey(KeyCode.E) && Vector3.Distance(selectedButton.transform.position, selectedButtonPushed.transform.position) > 0.025f)
            {
                selectedButton.transform.position = Vector3.Lerp(selectedButton.transform.position, selectedButtonPushed.transform.position, 1f * Time.deltaTime);
                float distance = Vector3.Distance(selectedButton.transform.position, selectedButtonPushed.transform.position);
                if (Vector3.Distance(selectedButton.transform.position, selectedButtonPushed.transform.position) < 0.025f)
                {
                    selectedButton.transform.position = selectedButtonPushed.transform.position;
                    selectedButton = noButtonSelected;
                    selectedButtonPushed = noButtonSelected;

                    buttonsPushed++;
                    
                }

                if (buttonsPushed == 3)
                {
                    TasksScript.tasksScriptInstance.electricalInspectionDone = true;
                    buttonsPushed = 0;
                    TasksScript.tasksScriptInstance.inspectTask2.text = "Inspection Done";
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        selectedButton = noButtonSelected;
        selectedButtonPushed = noButtonSelected;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        selectedButton = noButtonSelected;
    }
}
