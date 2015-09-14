using UnityEngine;
using System.Collections;

public class FriendlyUnit : BaseUnit {
    public override void Dead()
    {
        state = UnitState.DEAD;
        GameManager.ins.Lose();
        SetActiveObject(false);
    }

    public override void BombReaction()
    {
        //не реагируем на бомбу
    }

    public override void Finished()
    {
      ChangeState(UnitState.NONE);
    }
}
