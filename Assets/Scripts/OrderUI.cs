using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class OrderUI : MonoBehaviour
{
    public Order order;
    public Image image;
    public Slider slider;
    
    private void Start()
    {
        image.sprite = order.config.image;
        slider.maxValue = order.config.timeDuration;
    }

    private void Update()
    {
        if (slider.value > 0)
        {
            slider.value -= Time.deltaTime;
        }
    }

    public void DestoryDisplay()
    {
        Destroy(gameObject);
    }
}