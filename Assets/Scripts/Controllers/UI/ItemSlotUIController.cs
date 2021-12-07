using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUIController : MonoBehaviour
{
    public TextMeshProUGUI slotNumber;
    public TextMeshProUGUI itemCount;
    public Image itemIcon;
    public Image backgroundImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initializeSlot(int number, Color iconColor) {
        slotNumber.text = number.ToString();
        backgroundImage.color = iconColor;
    }
    public void initializeSlot(int number) {
        slotNumber.text = number.ToString();
    }
    public void UpdateData(short count, Sprite iconSprite, Color iconColor) {
        itemCount.text = count <= 1 ? "" : count.ToString();
        itemIcon.sprite = iconSprite;
        itemIcon.color = iconColor;
    }
}
