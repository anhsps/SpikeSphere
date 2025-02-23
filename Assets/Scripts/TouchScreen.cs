using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static TouchScreen instance { get; private set; }

    [HideInInspector] public bool click;
    private int soundIndex = 5;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        click = true;
        SoundManager8.instance.PlaySound(soundIndex);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        click = false;
    }
}
