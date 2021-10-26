using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PrimaryActionableObject
{
    void primaryAction_Start();
    void primaryAction_Hold();
    void primaryAction_Cancel();
}
