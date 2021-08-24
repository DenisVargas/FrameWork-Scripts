using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBehaviour : MonoBehaviour
{
    public Vector2 dragLimits;
    public Vector2 dragVector;
    RectTransform myRect = null;
    bool isDragging = false;

    public Vector2 originalPosition = Vector3.zero;

    private void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            var touch = Input.GetTouch(0);
            var pos = myRect.rect.position;

            print("Anchored Position: " + myRect.anchoredPosition);
            print("World Position" + myRect.position);

            Vector2 currentScreen = new Vector2( Screen.width, Screen.height);
            print("ScreenSpace Size: " + currentScreen);

            pos = touch.position;
            var posType2 = touch.deltaPosition;
            var posType3 = touch.rawPosition;
            print("touch Position is: " + touch.position);
            print("touch Delta Position is: " + touch.deltaPosition);
            print("touch Raw Position is: " + touch.rawPosition);
        }
    }
}
