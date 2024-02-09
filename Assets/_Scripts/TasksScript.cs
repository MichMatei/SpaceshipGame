using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TasksScript : MonoBehaviour
{
    public static TasksScript tasksScriptInstance;

    #region Variables
    [HideInInspector]
    public Vector3 RobotInspectElectricalPosition;
    [HideInInspector]
    public Quaternion RobotInspectElectricalRotation;
    [HideInInspector]
    public Vector3 RobotInspectOxigenPosition;
    [HideInInspector]
    public Quaternion RobotInspectOxigenRotation;
    [HideInInspector]
    public Vector3 RobotFixElectricalPosition;
    [HideInInspector]
    public Quaternion RobotFixElectricalRotation;
    [HideInInspector]
    public Vector3 RobotFixOxigenPosition;
    [HideInInspector]
    public Quaternion RobotFixOxigenRotation;
    [HideInInspector]
    public Vector3 RobotUpgradingPosition;
    [HideInInspector]
    public Quaternion RobotUpgradingRotation;

    public GameObject taskCanvas;
    public TextMeshProUGUI taskText;
    public TextMeshProUGUI taskText2;
    public TextMeshProUGUI taskText3;
    public TextMeshProUGUI eToInteract;

    public TextMeshProUGUI inspectTask;
    public TextMeshProUGUI inspectTask2;
    public TextMeshProUGUI inspectTask3;

    public Image fadeToBlack;
    public Slider staminaSlider;

    public Slider oxigenSlider;
    public Slider electricitySlider;

    [HideInInspector]
    public bool sleep = false;

    bool wakingUp = false;
    float myTime = 0f;

    [HideInInspector]
    public bool cleanShipTaskActive = false;
    [HideInInspector]
    public bool electricalTaskActive = false;
    [HideInInspector]
    public bool oxigenTaskActive = false;

    [HideInInspector]
    public bool inspectionActive = false;
    [HideInInspector]
    public bool electricalInspectionDone = true;
    [HideInInspector]
    public bool oxigenInspectionDone = true;
    [HideInInspector]
    public bool resetInspections = false;

    [HideInInspector]
    public bool robotFixElectrical = false;
    [HideInInspector]
    public bool robotFixOxigen = false;
    [HideInInspector]
    public bool robotInspectElectrical = false;
    [HideInInspector]
    public bool robotInspectOxigen = false;
    [HideInInspector]
    public bool robot1Upgrading = false;
    [HideInInspector]
    public bool robot2Upgrading = false;
    [HideInInspector]
    public bool resetRobots = false;

    int daysUpgrading = 0;
    #endregion

    const int chance = 25;
    int electricalMalfunctionChance = 0;
    int oxigenMalfunctionChance = 0;
    int cleanShipMalfunctionChance = 0;

    public void Awake()
    {
        if (tasksScriptInstance == null)
        {
            tasksScriptInstance = this;
            DontDestroyOnLoad(tasksScriptInstance);
        }
        else
        {
            Debug.Log("destro");//this needs a fix and into playermovement.cs
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        eToInteract.fontSize = 0;

        electricalInspectionDone = true;
        oxigenInspectionDone = true;

        electricitySlider.value = Random.Range(50, 75);
        oxigenSlider.value = Random.Range(50, 75);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            resetRobots = true;
            TaskSituationUpdate();
        }

        if (sleep)
        {
            tasksScriptInstance.robotFixOxigen = false;
            tasksScriptInstance.robotFixElectrical = false;
            myTime += .5f * Time.deltaTime;
            fadeToBlack.color = new Color(fadeToBlack.color.r, fadeToBlack.color.g, fadeToBlack.color.b, myTime);
        }

        if (myTime > 2)
        {
            wakingUp = true;
            sleep = false;
        }

        if (wakingUp)
        {
            myTime -= .5f * Time.deltaTime;
            sleep = false;
            fadeToBlack.color = new Color(fadeToBlack.color.r, fadeToBlack.color.g, fadeToBlack.color.b, myTime);
            if (myTime < 0f)
            {
                staminaSlider.value = staminaSlider.maxValue;
                wakingUp = false;
                myTime = 0f;

                TaskSituationUpdate();
                resetRobots = true;
            }
            
        }

        if (!sleep)
        {
            staminaSlider.value -= 1f * Time.deltaTime;
        }

        if (!oxigenTaskActive && oxigenSlider.value < oxigenSlider.maxValue)
        {
            oxigenSlider.value += 1f * Time.deltaTime;
        }
        else if (oxigenTaskActive && oxigenSlider.value > oxigenSlider.minValue)
        {
            oxigenSlider.value -= 1f * Time.deltaTime;
        }

        if (!electricalTaskActive && electricitySlider.value < electricitySlider.maxValue)
        {
            electricitySlider.value += 1f * Time.deltaTime;
        }
        else if (electricalTaskActive && electricitySlider.value > electricitySlider.minValue)
        {
            electricitySlider.value -= 1f * Time.deltaTime;
        }
    }

    //function that handles activating the malfunctions, checks for inspections done and switches rng and resets robots to their locations
    void TaskSituationUpdate()
    {
        if (electricalInspectionDone)
        {
            electricalMalfunctionChance = 0;
        }
        else
        {
            electricalMalfunctionChance += 25;
        }

        if (oxigenInspectionDone)
        {
            oxigenMalfunctionChance = 0;
        }
        else
        {
            oxigenMalfunctionChance += 25;
        }

        int cleanShipChance = chance + cleanShipMalfunctionChance;
        int fixEnergyChance = chance + electricalMalfunctionChance;
        int fixOxigenChance = chance + oxigenMalfunctionChance;

        resetInspections = true;

        if (cleanShipTaskActive)
        {
            //do nothing as the task is already active
            oxigenSlider.value -= 10f;
        }
        else
        {
            int cleanShipRng = Random.Range(1, 101);
            if (cleanShipRng <= cleanShipChance)
            {
                cleanShipTaskActive = true;
            }
            else
            {
                cleanShipTaskActive = false;
            }
        }

        if (electricalTaskActive)
        {
            //do nothing as the task is already active
            electricitySlider.value -= 20;
        }
        else
        {
            int fixEnergyRng = Random.Range(1, 101);
            if (fixEnergyRng <= fixEnergyChance)
            {
                electricalTaskActive = true;
            }
            else
            {
                electricalTaskActive = false;
            }
        }

        if (oxigenTaskActive)
        {
            //do nothing as the task is already active
            oxigenSlider.value -= 20;
        }
        else
        {
            int fixOxigenRng = Random.Range(1, 101);
            if (fixOxigenRng <= fixOxigenChance)
            {
                oxigenTaskActive = true;
            }
            else
            {
                oxigenTaskActive = false;
            }
        }


        if (cleanShipTaskActive)
        {
            taskText.text = "Clean Ship";
        }
        else
        {
            taskText.text = "No Task";
        }

        if (electricalTaskActive)
        {
            taskText2.text = "Fix Energy Room";
        }
        else
        {
            taskText2.text = "No Task";
        }

        if (oxigenTaskActive)
        {
            taskText3.text = "Fix Oxigen Room";
        }
        else
        {
            taskText3.text = "No Task";
        }

        inspectTask.text = "No Inspection Needed";
        inspectTask2.text = "Run Electrical inspection";
        inspectTask3.text = "Run Oxigen inspection";

        if (robot1Upgrading && daysUpgrading <= 2)
        {
            electricitySlider.value -= 30f;
            daysUpgrading++;

            if (daysUpgrading == 3)
            {
                daysUpgrading = 0;
                robot1Upgrading = false;
            }
        }
        else if (robot2Upgrading && daysUpgrading <= 2)
        {
            electricitySlider.value -= 30f;
            daysUpgrading++;

            if (daysUpgrading == 3)
            {
                daysUpgrading = 0;
                robot2Upgrading = false;
            }
        }

        oxigenInspectionDone = false;
        electricalInspectionDone = false;
    }

    public void StaminaSliderUpdater(float stamina, Slider displaySlider)
    {
        //displaySlider.value = stamina;
    }
}
