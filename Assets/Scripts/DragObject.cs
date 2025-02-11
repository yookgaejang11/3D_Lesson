using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragObject : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IEndDragHandler,
    IBeginDragHandler
{

    public Transform parentTr;

    public Vector2 beginPoint;
    private Vector2 moveBegin;
    private Transform beginParent;
    GameObject canvas;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        if (parentTr == null)
        {
            parentTr = this.transform.parent;
        }
        canvas = GameObject.Find("Canvas");
        canvasGroup = this.GetComponent<CanvasGroup>();
    }


    //드래그 : 마우스 커서 위치로 이동
    public void OnDrag(PointerEventData eventData)
    {
        parentTr.position = beginPoint + (eventData.position -moveBegin);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if(this.GetComponent<RectTransform>().parent.name == "Inventory")
        {
            this.transform.SetParent(beginParent);
            this.GetComponent <RectTransform>().anchoredPosition = Vector2.zero;
           
        }
        if (this.GetComponent<RectTransform>().parent.childCount > 2)
        {
            Transform childObject = this.transform.parent.GetChild(1);
            childObject.SetParent(beginParent);
            childObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            

        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        beginPoint = parentTr.position;
        beginParent = this.transform.parent;
        moveBegin = eventData.position;
        if (parentTr == this.transform)
        {
            this.GetComponent<RectTransform>().SetParent(this.transform.parent.parent);
        }
    }
}
