using UnityEngine;

public class GoingToSleep : MonoBehaviour
{
    bool inRange = false;
    TasksScript myTask;

    private void Start()
    {
        //myTask = TasksScript.tasksScriptInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && TasksScript.tasksScriptInstance.staminaSlider.value <= 25f)
        {
            TasksScript.tasksScriptInstance.sleep = true;
            TasksScript.tasksScriptInstance.staminaSlider.value = TasksScript.tasksScriptInstance.staminaSlider.maxValue;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
