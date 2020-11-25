using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class GamePlayButton : IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    UnityEvent onPointerDown;
    [SerializeField]
    UnityEvent onPointerUp;
    [SerializeField]
    UnityEvent onPointerHold;

    bool isHolding;
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        onPointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        onPointerUp?.Invoke();
    }

    void Update() {
        if (isHolding) {
            onPointerHold?.Invoke();
        }
    }
}
