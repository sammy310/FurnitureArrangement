using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_SizeMeasureUI : MonoBehaviour
{
    [SerializeField] SizeCubeManager sizeCubeManager;

    [SerializeField] GameObject sizeMeasureUIPanel;

    [SerializeField] TMP_InputField widthInputField;
    [SerializeField] TMP_InputField heightInputField;
    [SerializeField] TMP_InputField depthInputField;

    [SerializeField] Button createButton;
    RectTransform createButtonRect = null;

    public const int MinSize = 10;
    public const int MaxSize = 10000;


    private void Awake()
    {
        widthInputField.onEndEdit.AddListener(WidthInputFieldCorrect);
        heightInputField.onEndEdit.AddListener(HeightInputFieldCorrect);
        depthInputField.onEndEdit.AddListener(DepthInputFieldCorrect);

        createButtonRect = createButton.GetComponent<RectTransform>();
    }

    public void EnableUI()
    {
        if (sizeCubeManager.IsSizeCubeActive)
        {
            sizeCubeManager.DisableSizeCube();
            return;
        }

        if (sizeCubeManager.CreateSizeCubeFromCurrentFurniture())
        {
            DisableUI();
            ResetInputField();
            return;
        }

        //sizeMeasureUIPanel.SetActive(true);
    }

    public void DisableUI()
    {
        sizeMeasureUIPanel.SetActive(false);
    }

    void ResetInputField()
    {
        widthInputField.text = string.Empty;
        heightInputField.text = string.Empty;
        depthInputField.text = string.Empty;
    }


    public void CreateButton()
    {
        if (!int.TryParse(widthInputField.text, out int width))
        {
            return;
        }
        if (!int.TryParse(heightInputField.text, out int height))
        {
            return;
        }
        if (!int.TryParse(depthInputField.text, out int depth))
        {
            return;
        }

        if (width < MinSize || height < MinSize || depth < MinSize) return;

        sizeCubeManager.CreateSizeCubeWithMilimeter(width, height, depth);

        DisableUI();
        ResetInputField();
    }


    int GetInputFieldCorrect(string inputValue)
    {
        if (int.TryParse(inputValue, out int value))
        {
            return Mathf.Max(MinSize, Mathf.Min(MaxSize, value));
        }
        return 0;
    }

    void WidthInputFieldCorrect(string inputValue)
    {
        int width = GetInputFieldCorrect(inputValue);
        widthInputField.text = width.ToString();
    }

    void HeightInputFieldCorrect(string inputValue)
    {
        int height = GetInputFieldCorrect(inputValue);
        heightInputField.text = height.ToString();
    }

    void DepthInputFieldCorrect(string inputValue)
    {
        int depth = GetInputFieldCorrect(inputValue);
        depthInputField.text = depth.ToString();
    }


    public void SetActiveCreateButton(bool value)
    {
        createButton.gameObject.SetActive(value);
    }

    public void UpdateCreateButton(bool activate)
    {
        if (activate)
            createButtonRect.localRotation = Quaternion.Euler(0, 0, 45);
        else
            createButtonRect.localRotation = Quaternion.identity;
    }
}
