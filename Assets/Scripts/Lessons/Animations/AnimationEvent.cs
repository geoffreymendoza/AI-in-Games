using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    LessonAIFollowPatrol controller;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponentInParent<LessonAIFollowPatrol>();
    }

    public void SpawnMeleeVFX()
    {
        controller.MeleeVFX();
    }
}
