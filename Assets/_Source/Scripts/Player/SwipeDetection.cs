using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector2 direction);

    private Vector2 _tapPosition;
    private Vector2 _swipeDelta;

    private readonly float DeadZone = 50;
    private bool _isSwiping;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _isSwiping = true;
                _tapPosition = Input.GetTouch(0).position;
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Canceled ||
                Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                ResetSwipe();
            }
        }

        CheckSwipe();
    }

    private void CheckSwipe()
    {
        if (_isSwiping)
        {
            if (Input.touchCount > 0)
                _swipeDelta = Input.GetTouch(0).position - _tapPosition;
        }

        if(_swipeDelta.magnitude > DeadZone)
        {
            if(SwipeEvent != null)
            {
                if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                    SwipeEvent(_swipeDelta.x > 0 ? Vector2.right : Vector2.left);
                else
                    SwipeEvent(_swipeDelta.y > 0 ? Vector2.up : Vector2.down);
            }

            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        _isSwiping = false;

        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }
}