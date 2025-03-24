using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashingControllersImages : MonoBehaviour
{
    //Eden: images for controllers off
    public Image[] controllersOffImages;

    //Eden: images for controllers on
    public Image[] controllersOnImages;

    //Eden: seconds between flashing
    public float flashInterval = 1.0f;

    //Eden: this bool tracks which group off images is currently active
    private bool isControllerImageOff = true;

    void Start()
    {
        //eden: Controller OFF image active, Controller ON image inactive
        SetGroupActive(isControllerImageOff);
        //Eden: start flashing
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        while (true)
        {
            //Eden: wait for time interval 
            yield return new WaitForSeconds(flashInterval);
            //Eden: set active state
            isControllerImageOff = !isControllerImageOff;
            SetGroupActive(isControllerImageOff);
        }
    }

    private void SetGroupActive(bool controllerOff)
    {
        //Eden: activate or deactivate all controllerOffImages
        foreach (var img in controllersOffImages)
        {
            if (img != null)
                img.gameObject.SetActive(controllerOff);
        }
        //Eden: activate or deactivate all controllerOnImages
        foreach (var img in controllersOnImages)
        {
            if (img != null)
                img.gameObject.SetActive(!controllerOff);
        }
    }
}
