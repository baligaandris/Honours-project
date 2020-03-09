using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class ContactNameSwitcher : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    [YarnCommand("changename")]
    public void ChangeName(string newName)
    {
        nameText.text = newName;
    }
}
