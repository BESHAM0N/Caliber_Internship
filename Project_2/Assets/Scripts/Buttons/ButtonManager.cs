using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public event Action Click;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    //private void UpdateText(int price) => _text.text = price.ToString();

    private void OnButtonClick()
    {
        // добавить проверку на невозможность купить айтем и менять подцветку в соответсвии
        Click?.Invoke();
    }
}