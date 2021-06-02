using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightsHandler : Singleton<LightsHandler>
{
    [SerializeField] UnityEvent onAllLightsAreOn;

    static int lightsCount = 18;
    int lightsOnCount = 0;
    
    // Prevent non-singleton constructor use.
    protected LightsHandler() { }

    public void IncreaseLightsOnCount() {
        lightsOnCount++;
        if (lightsCount == lightsOnCount) {
            onAllLightsAreOn.Invoke();
        }
    }

}
