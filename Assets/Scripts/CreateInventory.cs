using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateInventory : MonoBehaviour
{
    public bool isOpen = false;
    //슬롯 개수
    public int slotCntWidth = 4;
    public int slotCntHeight = 4;

    //슬롯 사이즈
    public int slotWidthSize = 64;
    public int slotHeightSize = 64;

    //슬롯 프리팹
    public GameObject prefabslot;
    public RectTransform parentObj;
    public Sprite backSprite;
    public int SlotTopBarSize = 20;
    GameObject inventory;
    public List<GameObject> slots = new List<GameObject>();
    public GameObject prefabItem;
    public List<Item> item = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        SetInventory();

        
        Item i = new Item();
        item.Add(i);


        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            inventory.SetActive(true);
            SetItem(item);
            isOpen = true;
        } else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventory.SetActive(false);
            isOpen = false;
        }

        
    }

    void SetInventory()
    {
        inventory = new GameObject();
        Image image = inventory.AddComponent<Image>();
        if(backSprite != null)
        {
            image.sprite = backSprite;
        }
        RectTransform inventoryRT =
            inventory.GetComponent<RectTransform>();
        inventoryRT.SetParent(parentObj);
        inventoryRT.anchoredPosition = Vector3.zero;
        inventoryRT.sizeDelta =
            new Vector2(slotWidthSize * slotCntWidth, slotHeightSize * slotCntHeight + SlotTopBarSize);
        inventoryRT.name = "Inventory";

        //인벤토리 움직일 헤더 생성
        GameObject backTop = new GameObject();
        Image backTopImg = backTop.AddComponent<Image>();
        backTopImg.color = Color.blue;
        RectTransform backTopRT = backTop.GetComponent<RectTransform>();
        backTopRT.SetParent(inventoryRT);
        backTopRT.pivot = Vector2.up;
        backTopRT.anchorMin = Vector2.up;
        backTopRT.anchorMax = Vector2.up;
        backTopRT.anchoredPosition = Vector3.zero;
        backTopRT.sizeDelta =
            new Vector2(slotWidthSize * slotCntWidth, SlotTopBarSize);
        backTop.AddComponent<DragObject>();

        for (int i = 0; i < slotCntHeight; i++)
        {
            for (int j = 0; j < slotCntWidth; j++)
            {
                GameObject slot = Instantiate(prefabslot);
                RectTransform slotRt =
                    slot.GetComponent<RectTransform>();
                slotRt.SetParent(inventoryRT);
                slotRt.pivot = Vector2.up;
                slotRt.anchorMin = Vector2.up;
                slotRt.anchorMax = Vector2.up;
                slotRt.anchoredPosition= Vector3.zero;
                slotRt.anchoredPosition +=
                    new Vector2(slotWidthSize * i, -(slotHeightSize * j) - SlotTopBarSize);
                slotRt.sizeDelta = 
                    new Vector2(slotWidthSize, slotHeightSize);
                slotRt.name = i + "  ,  " + j;
                slots.Add(slot);
            }
        }
    }

    public void SetItem(List<Item> item)
    {
        for(int i = 0;i < item.Count;i++)
        {
            GameObject it = Instantiate(prefabItem);
            RectTransform itRt = it.GetComponent<RectTransform>();
            itRt.SetParent(slots[i].GetComponent<RectTransform>());
            itRt.pivot = new Vector2(0.5f, 0.5f);
            itRt.anchorMin = Vector2.zero;
            itRt.anchorMax = Vector2.one;
            itRt.offsetMin = new Vector2(10, 10);
            //offseMax의 경우 반대
            itRt.offsetMax = new Vector2(-10, -10);
            it.AddComponent<DragObject>().parentTr = it.transform;
            
        }
    }
}
