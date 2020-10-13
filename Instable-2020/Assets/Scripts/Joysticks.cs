using UnityEngine.EventSystems;
using UnityEngine;
public class Joysticks : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    /// <summary>
    /// Put it on any Image UI Element
    /// </summary>
    protected RectTransform Background;

    public bool Pressed;

    protected int PointerId;
    public RectTransform Handle;
    public string joystickname;

    bool isknifethrow = false;

    int joystickmovementflag = 0;

    [Range(0f, 3f)]
    public float HandleRange = .6f;

    [HideInInspector]
    public Vector2 InputVector = Vector2.zero;
    public Vector2 AxisNormalized 
    { get
        {
            return InputVector.magnitude > 0.25f ? InputVector.normalized : (InputVector.magnitude < 0.01f ? Vector2.zero : InputVector * 4f); 
        } 
    }

    void Start()
    {
        if (Handle == null)
            Handle = transform.GetChild(0).GetComponent<RectTransform>();
        Background = GetComponent<RectTransform>();
        Background.pivot = new Vector2(0.5f, 0.5f);
        Pressed = false;
    }

    void Update()
    {
        if (Pressed)
        {
            Vector2 direction = (PointerId >= 0 && PointerId < Input.touches.Length) ? Input.touches[PointerId].position - new Vector2(Background.position.x, Background.position.y) : new Vector2(Input.mousePosition.x, Input.mousePosition.y) - new Vector2(Background.position.x, Background.position.y);
            InputVector = (direction.magnitude > Background.sizeDelta.x / 2f) ? direction.normalized : direction / (Background.sizeDelta.x / 2f);
            Handle.anchoredPosition = (InputVector * Background.sizeDelta.x / 2f) * HandleRange;

            if (Handle.position.x > transform.position.x)
                joystickmovementflag = 1;
            else if (Handle.position.x < transform.position.x)
                joystickmovementflag = -1;
            else
                joystickmovementflag = 0;


            if (joystickname == "Right")
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(), Handle.position))
                {
                    isknifethrow = true;
                }
                else
                    isknifethrow = false;
            }
            else
                isknifethrow = false;
        }
        else
        {
            joystickmovementflag = 0;
            isknifethrow = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        InputVector = Vector2.zero;
        Handle.anchoredPosition = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (FindObjectOfType<Hero>())
        {
            if (joystickname == "Left")
                FindObjectOfType<Hero>().Input_move = joystickmovementflag;

            if (isknifethrow && joystickname == "Right")
            {
                if (FindObjectsOfType<Knife>().Length < 1)
                    FindObjectOfType<Hero>().SpawnKnife(InputVector.normalized, joystickmovementflag);

            }
        }
    }
}