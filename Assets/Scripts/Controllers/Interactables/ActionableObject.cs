using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ActionableObject
{
    void primaryAction_Single();
    void primaryAction_Hold();
    void primaryAction_Canceled();
    void secondaryAction_Single();
    void secondaryAction_Hold();
    void secondaryAction_Canceled();
}
