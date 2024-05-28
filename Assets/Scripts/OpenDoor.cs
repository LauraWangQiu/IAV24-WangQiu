using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private string tagName = "Client";
    [SerializeField] private Animator openandclose1;
    public bool isOpen = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            if (openandclose1 != null)
            {
                //StartCoroutine(opening());
                openandclose1.Play("Opening 1");
                isOpen = true;
            }
            else
            {
                Debug.LogWarning("Door Animator not found");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            if (openandclose1 != null)
            {
                //StartCoroutine(closing());
                openandclose1.Play("Closing 1");
                isOpen = false;
            }
            else
            {
                Debug.LogWarning("Door Animator not found");
            }
        }
    }

    IEnumerator opening()
    {
        openandclose1.Play("Opening 1");
        isOpen = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator closing()
    {
        openandclose1.Play("Closing 1");
        isOpen = false;
        yield return new WaitForSeconds(.5f);
    }
}
