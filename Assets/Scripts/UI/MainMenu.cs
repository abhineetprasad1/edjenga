using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class MainMenu : MonoBehaviour
{

    [SerializeField] Transform infoBox;
    [SerializeField] TextMeshProUGUI infoDescription;

    private void OnEnable()
    {
        GameEvents.OnJblockClick += OnInfoDisplay;
    }

    private void OnDisable()
    {
        GameEvents.OnJblockClick -= OnInfoDisplay;
    }

    public void OnInfoCloseClick()
    {
        infoBox.gameObject.SetActive(false);
    }

    void OnInfoDisplay(Dictionary<string, object> data)
    {
        if (data == null)
        {
            infoBox.gameObject.SetActive(false);
        }
        else
        {

            var jBlock = (JBlock)data["jBlock"];

            // Create a formatted string using StringBuilder
            StringBuilder stringBuilder = new StringBuilder();

            // Append the formatted string
            stringBuilder.AppendLine($"{jBlock.grade}: {jBlock.domain}");
            stringBuilder.AppendLine($"{jBlock.cluster}");
            stringBuilder.AppendLine($"{jBlock.standardid}:{jBlock.standarddescription}");

            // Get the final string
            infoDescription.text = stringBuilder.ToString();


            infoBox.transform.position = (Vector3)data["pos"];
            infoBox.gameObject.SetActive(true);
        }
    }

    public void OnRightArrowClick()
    {
        var data = new Dictionary<string, object>();
        data["next"] = true;
        GameEvents.OnArrowClick(data);
    }

    public void OnLeftArrowClick()
    {

        var data = new Dictionary<string, object>();
        data["next"] = false;
        GameEvents.OnArrowClick(data);

    }

    public void OnTestMyStack()
    {
        GameEvents.OnStackTest(null);
    }

    public void OnReset()
    {
        GameEvents.OnReset(null);
    }
}
