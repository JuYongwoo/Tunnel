using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : UIButtonBinder<SettingsUI.ButtonID>
{
    private GameObject settingsScreen;

    private GameObject VideoSettings;
    private GameObject SoundSettings;
    private GameObject InfoSettings;

    private Dropdown resolutionDropdown;
    private Toggle fullscreenToggle;
    private bool isFullScreen = false;


    private readonly Vector2Int[] resolutions = new Vector2Int[]
{
        new Vector2Int(1920, 1080),
        new Vector2Int(1600, 900)
};

    public enum ButtonID
    {
        SettingExitButton,
        VideoButton,
        SoundButton,
        InfomationButton,
    }

    protected override void Awake()
    {
        base.Awake();

        var all = transform.root.GetComponentsInChildren<Transform>(true);

        GameObject Find(string name) => all.FirstOrDefault(t => t.name == name)?.gameObject;
        settingsScreen = Find("SettingsCanvas");
        VideoSettings = Find("VideoSettings");
        SoundSettings = Find("SoundSettings");
        InfoSettings = Find("InfoSettings");

        resolutionDropdown = transform.root.GetComponentsInChildren<Dropdown>(true).FirstOrDefault(d => d.name == "VideoDropdown");
        fullscreenToggle = transform.root.GetComponentsInChildren<Toggle>(true).FirstOrDefault(t => t.name == "Toggle");

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(new List<string> { "1920 x 1080", "1600 x 900" });

        settingsScreen.SetActive(false);
        VideoButtonClick();
    }


    protected override Dictionary<ButtonID, UnityAction> GetHandlers()
    {
        return new Dictionary<ButtonID, UnityAction>
        {
            { ButtonID.SettingExitButton, () => { settingsScreen.SetActive(false); } },
            { ButtonID.VideoButton, VideoButtonClick },
            { ButtonID.SoundButton, SoundButtonClick },
            { ButtonID.InfomationButton, InformationButtonClick }
        };
    }

    public void InitVidoeSettings()
    {
        //�ʱ�ȭ //���������� ������ Resolution�� Fullscreen ���θ� ���Ͽ� �����Ͽ� ó���ϵ��� Ȯ�� ����
        resolutionDropdown.value = 0;
        resolutionDropdown.RefreshShownValue();
        ApplyResolution(resolutionDropdown.value);

        //�ʱ�ȭ
        fullscreenToggle.isOn = false;
        ToggleFullscreen(fullscreenToggle.isOn);

        // �ػ� ���� ��
        resolutionDropdown.onValueChanged.AddListener(index =>
        {
            ApplyResolution(index);
        });

        // ��üȭ�� ��� ��
        fullscreenToggle.onValueChanged.AddListener(isFullscreen =>
        {
            ToggleFullscreen(isFullscreen);
        });

    }

    public void ToggleFullscreen(bool fullscreen)
    {
        isFullScreen = fullscreen;
        Screen.fullScreen = isFullScreen;
        Screen.fullScreenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    // �ε��� ��� �ػ� ���� (Dropdown ���� ��)
    public void ApplyResolution(int index)
    {
        if (index < 0 || index >= resolutions.Length) return;

        Vector2Int res = resolutions[index];
        Screen.SetResolution(res.x, res.y, isFullScreen);
    }



    public void VideoButtonClick()
    {
        SoundSettings.SetActive(false);
        InfoSettings.SetActive(false);
        VideoSettings.SetActive(true);
    }

    public void SoundButtonClick()
    {
        VideoSettings.SetActive(false);
        InfoSettings.SetActive(false);
        SoundSettings.SetActive(true);
    }

    public void InformationButtonClick()
    {
        VideoSettings.SetActive(false);
        SoundSettings.SetActive(false);
        InfoSettings.SetActive(true);
    }
}
