using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlacementArea : MonoBehaviour
{
    [SerializeField] GameObject feedbackMessageUI; // Feedback message when the player puts an object in place 
    [SerializeField] string objectGrabbableTag;
    private List<ObjectGrabbable> objectGrabbables = new List<ObjectGrabbable>(); // List of ObjectGrabbable
    private ScoreManager scoreManager;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        scoreManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<ScoreManager>(); // init score manager
        feedbackMessageUI.SetActive(false); // disable feedback message
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(objectGrabbableTag); // Get the list of objects belonging to the placement area
        foreach (GameObject obj in objectsWithTag)
        {
            ObjectGrabbable grabbable = obj.GetComponent<ObjectGrabbable>(); // get the ObjectGrabbable component
            if (grabbable != null)
            {
                objectGrabbables.Add(grabbable); // add it to the ObjectGrabbable List
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (ObjectGrabbable grabbable in objectGrabbables)
        {
            // Checking whether the object in the list is not in place
            if (!grabbable.IsInPlace())
            {
                // Checking whether the incoming collider matches the collider of the current object
                if (other == grabbable.GetComponent<Collider>())
                {
                    scoreManager.AddPoint(); // add point 
                    grabbable.DisablePlacementArrow(); // for tutorial mode
                    grabbable.SetObjectInPlace(true); // Set the state of the object to in place
                    feedbackMessageUI.SetActive(true); // Enable the feedback message for the player
                    audioManager.PlaySFX(audioManager.placement);
                    Invoke("SetInactiveUI", 2f); // Disable the feedback message afer 2 seconds
                    // Call function to hide the object after a delay
                    HideObjectAfterDelay(grabbable, 5f); // Hide the object after 5 seconds
                }
            }
        }
    }

    // Disable the feedback message
    private void SetInactiveUI()
    {
        feedbackMessageUI.SetActive(false);
    }

    // Function to hide the object after a delay
    private void HideObjectAfterDelay(ObjectGrabbable grabbable, float delay)
    {
        StartCoroutine(HideObjectDelayed(grabbable, delay));
    }

    // Coroutine to hide the object after a delay
    private IEnumerator HideObjectDelayed(ObjectGrabbable grabbable, float delay)
    {
        yield return new WaitForSeconds(delay);
        // Hide the object
        grabbable.gameObject.SetActive(false);
    }


}
