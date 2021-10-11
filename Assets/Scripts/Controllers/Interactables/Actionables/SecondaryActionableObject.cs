using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SecondaryActionableObject
{
    void secondaryAction_Single();
    void secondaryAction_Hold();
    void secondaryAction_Canceled();
}
