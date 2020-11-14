using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, GetYOnSelectedButton(), transform.localPosition.z);
    }

    private float GetYOnSelectedButton()
    {
        foreach (Button button in buttons.Where(button => button.gameObject == EventSystem.current.currentSelectedGameObject))
        {
            return button.transform.localPosition.y;
        }
        return transform.localPosition.y;
    }
}
