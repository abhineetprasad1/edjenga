using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GradeLabelViewController : ViewController
{
    [SerializeField]
    TextMeshPro gradeLabel;

    public void SetLabel(string text)
    {
        gradeLabel.text = text;
    }
    
}
