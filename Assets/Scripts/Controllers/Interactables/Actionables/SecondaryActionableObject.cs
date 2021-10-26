using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SecondaryActionableObject
{
    void secondaryAction_Start();
    void secondaryAction_Hold();
    void secondaryAction_Cancel();
}
