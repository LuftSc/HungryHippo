using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveHippo : MonoBehaviour
{
    [Header("Картинка с бегемотом, у которого закрыт рот")]
    [SerializeField] private Sprite HippoClose;
    
    private Image HippoImage;
    private Sprite HippoOpen;
    private bool isClose = false;
    private void Awake()
    {
        HippoImage = gameObject.GetComponent<Image>();
        HippoOpen = HippoImage.sprite;
    }

    public void OnClick()
    {
        // Если бегемот с закрытым ртом, то меняем на открытый, и наоборот
        HippoImage.sprite = isClose ? HippoOpen : HippoClose;
        isClose = !isClose;
    }
    
}
