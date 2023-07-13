using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RectTransformTest : MonoBehaviour
{

    RectTransform rect_t;
    List<RectTransform> units = new List<RectTransform>();

    [SerializeField]
    private RectTransform rectSizeArea;
    [SerializeField]
    private Scrollbar scroll;
    [SerializeField]
    private bool updateContainer = false;

    private float RECT_T_HEIGHT = 931;

    private bool firstFrame = true;
    private bool doOnce = true;

    void Awake()
    {
        rect_t = GetComponent<RectTransform>();
        //RECT_T_HEIGHT = rectSizeArea.rect.height;
        //UpdateContainerSize();
    }

    private void Start()
    {
        UpdateContainerSize();
    }

    private void Update()
    {
        if (updateContainer)
        {
            UpdateContainerSize();
            updateContainer = false;
        }
    }

    private void SearchForUnits()
    {
        RectTransform[] tempRects = GetComponentsInChildren<RectTransform>();
        units.Clear();
        foreach (var t_rect in tempRects)
        {
            if (t_rect.gameObject.tag == "UIUnit")
            {
                units.Add(t_rect);
            }
        }
    }

    public void UpdateContainerSize()
    {
        Canvas.ForceUpdateCanvases();
        RECT_T_HEIGHT = rectSizeArea.rect.height;
        SearchForUnits();
        if (units.Count > 0)
        {
            rect_t.sizeDelta = new Vector2(rect_t.sizeDelta.x, (units[0].rect.height * units.Count) - RECT_T_HEIGHT);
            scroll.value = 1;
        }
    }
}
