using UnityEngine;

namespace CubeChan {
public static class Data {
    public static int IDLE_ANIMATION = Animator.StringToHash("Def_Idle");
    public static int RUN_ANIMATION = Animator.StringToHash("Def_Run");
    public static int THROW_ANIMATION = Animator.StringToHash("Def_Throw");
    public static int DEATH_ANIMATION = Animator.StringToHash("Def_Death");
}

public enum Team {
    Neutral,
    Red,
    Green
}
}