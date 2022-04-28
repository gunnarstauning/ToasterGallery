using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarIncrementer : MonoBehaviour
{
    public Scrollbar Target;
    public Button upButton;
    public Button downButton;
    public float Step = .05f;


    public void Increment()
    {
        if (Target == null || upButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        Target.value = Mathf.Clamp(Target.value + Step, 0, 1);
        upButton.interactable = true;
    }

    public void Decrement()
    {
        if (Target == null || downButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        Target.value = Mathf.Clamp(Target.value - Step, 0, 1);
        downButton.interactable = true;
    }
}
