using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;
using UnityEngine.Events;

public class TweenMove : MonoBehaviour
{
    public Vector3 fromPosition = Vector3.zero;
    public Vector3 toPosition = Vector3.forward;
    public Space space = Space.Self;
    public bool forceFromPosition;
    public float duration = 0.4f;
    public float delay = 0;
    public bool playOnStart = false;
    public Tween.LoopType loopType = Tween.LoopType.None;
    public AnimationCurve easing = Tween.EaseInOut;
    public Renderer rend;
    public UnityEvent startCallback;
    public UnityEvent completeCallback;

    private TweenBase _tween;

    private void Start()
    {
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (playOnStart)
        {
            Play();
        }
    }

    public void Play()
    {
        if (forceFromPosition)
        {
            if (space == Space.Self)
                _tween = Tween.LocalPosition(this.transform, fromPosition, toPosition, duration, delay, easing,
                    loopType,
                    delegate { startCallback?.Invoke(); }, delegate { completeCallback?.Invoke(); });
            else
                _tween = Tween.Position(this.transform, fromPosition, toPosition, duration, delay, easing, loopType,
                    delegate { startCallback?.Invoke(); }, delegate { completeCallback?.Invoke(); });
        }
        else
        {
            if (space == Space.Self)
                _tween = Tween.LocalPosition(this.transform, toPosition, duration, delay, easing, loopType,
                    delegate { startCallback?.Invoke(); }, delegate { completeCallback?.Invoke(); });
            else
                _tween = Tween.Position(this.transform, toPosition, duration, delay, easing, loopType,
                    delegate { startCallback?.Invoke(); }, delegate { completeCallback?.Invoke(); });
        }
    }

    public void PlayReverse()
    {
        if (forceFromPosition)
        {
            if (space == Space.Self)
                _tween = Tween.LocalPosition(this.transform, toPosition, fromPosition, duration, delay, easing,
                    loopType,
                    delegate { startCallback?.Invoke(); }, delegate { completeCallback?.Invoke(); });
            else
                _tween = Tween.Position(this.transform, toPosition, fromPosition, duration, delay, easing, loopType,
                    delegate { startCallback?.Invoke(); }, delegate { completeCallback?.Invoke(); });
        }
        else
        {
            if (space == Space.Self)
                _tween = Tween.LocalPosition(this.transform, fromPosition, duration, delay, easing, loopType,
                    delegate { startCallback?.Invoke(); }, delegate { completeCallback?.Invoke(); });
            else
                _tween = Tween.Position(this.transform, fromPosition, duration, delay, easing, loopType,
                    delegate { startCallback?.Invoke(); }, delegate { completeCallback?.Invoke(); });
        }
    }
}