using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICursor : MonoBehaviour
{
    [Tooltip("The RectTransform of the cursor icon")]
    [SerializeField] private RectTransform rect;
    
    //The image of the cursor icon
    private Image image;
    //The canvas the cursor icon is on
    private Canvas canvas;

    //Gets the RectTransform of the canvas
    private RectTransform canvasRectTransform => canvas.transform as RectTransform;

    //Hold reference to the actual screen size, the rendered screen size, and the ratio between the two
    //This is required to correctly place the cursor icon on the screen
    Vector2 screenSize;
    Vector2 refSize;
    Vector2 scale;

    void Start()
    {
        //Get the cursor icon from the given rect
        image = rect.GetComponent<Image>();

        //Get the canvas
        canvas = GetComponent<Canvas>();
        Initialise();
    }

    private void Initialise()
    {
        //Get how big the canvas is in pixels (this is often smaller than the screen)
        refSize = canvasRectTransform.rect.size;
        
        //Get how big the screen is in pixels
        screenSize = canvas.renderingDisplaySize;

        //Set the relative scale using the two sizes
        scale = new(screenSize.x / refSize.x, screenSize.y / refSize.y);
    }

    public void Update()
    {
        //If there's a mismatch between the current size of the screen and our saved size of the screen,
        //Re-initialise our screen-size values
        if (canvasRectTransform.rect.width != screenSize.x)
            Initialise();

        bool isOverUI = EventSystem.current.IsPointerOverGameObject();

        //Don't display the image if we're over UI or we're off the sides of the screen
        image.enabled = !isOverUI && WithinScreen(Input.mousePosition);

        //Update the position of the cursor using the mouse's screen position,
        //and the relative scale we worked out earlier
        rect.anchoredPosition = new(Input.mousePosition.x / scale.x, Input.mousePosition.y / scale.y);
    }

    /// <summary>
    /// Returns true if the given Vector2 is within the boundaries of the screen.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool WithinScreen(Vector2 position)
    {
        return position.x >= 0 && position.x <= Screen.width &&
            position.y >= 0 && position.y <= Screen.height;
    }
}
