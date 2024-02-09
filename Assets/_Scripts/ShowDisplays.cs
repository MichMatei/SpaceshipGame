using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShowDisplays : MonoBehaviour
{
    public GameObject display;

    public Slider staminaSlider;
    public Slider oxigenSlider;
    public Slider electricitySlider;
    public TextMeshProUGUI taskText;
    public TextMeshProUGUI taskText2;
    public TextMeshProUGUI taskText3;

    public TextMeshProUGUI inspectTask;
    public TextMeshProUGUI inspectTask2;
    public TextMeshProUGUI inspectTask3;

    // Start is called before the first frame update
    void Start()
    {
        display.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (display.activeInHierarchy)
        {
            staminaSlider.value = TasksScript.tasksScriptInstance.staminaSlider.value;
            oxigenSlider.value = TasksScript.tasksScriptInstance.oxigenSlider.value;
            electricitySlider.value = TasksScript.tasksScriptInstance.electricitySlider.value;

            taskText.text = TasksScript.tasksScriptInstance.taskText.text;
            taskText2.text = TasksScript.tasksScriptInstance.taskText2.text;
            taskText3.text = TasksScript.tasksScriptInstance.taskText3.text;

            inspectTask.text = TasksScript.tasksScriptInstance.inspectTask.text;
            inspectTask2.text = TasksScript.tasksScriptInstance.inspectTask2.text;
            inspectTask3.text = TasksScript.tasksScriptInstance.inspectTask3.text;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        display.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        display.SetActive(false);
    }
}
