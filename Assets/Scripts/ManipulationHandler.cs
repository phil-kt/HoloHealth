using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ManipulationHandler : MonoBehaviour, IManipulationHandler
{

    [Tooltip("Rotation max speed controls amount of rotation.")]
    private Vector3 manipulationPreviousPosition;

    private float rotationFactor;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        manipulationPreviousPosition = eventData.CumulativeDelta;
        this.transform.localScale = new Vector3(0.3515625f, 0.09765625f, 0.009765625f);
    }
    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        transform.position += eventData.CumulativeDelta;
        this.transform.localScale = new Vector3(1.0f, 1.3f, 1.3f);
    }
    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        Debug.Log("Manipulation is completed");
        this.transform.localScale = new Vector3(0.3515625f, 0.09765625f, 0.009765625f);
    }
    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        Debug.Log("Manipulation is canceled");
        this.transform.localScale = new Vector3(0.3515625f, 0.09765625f, 0.009765625f);
    }
}
