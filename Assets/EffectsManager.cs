using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EffectsManager : MonoBehaviour
{

    public static EffectsManager ins;

    public CanvasGroup minusLifeCanvasEffect;
    void Awake()
    {
        //Singletone
        if (ins == null) { ins = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); }

    }

    public void MinusLifeEffect()
    {

        DOTween.To(() => minusLifeCanvasEffect.alpha, x => minusLifeCanvasEffect.alpha = x, 1, 0.3f)
            .SetLoops(2, LoopType.Yoyo);
    }
}
