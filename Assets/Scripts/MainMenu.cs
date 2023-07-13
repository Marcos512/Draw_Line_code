using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenu : MonoBehaviour
{
    [SerializeField, Tooltip("����� ���������� ������")]
    private RectTransform _buttonsGrid;

    [SerializeField]
    private GameObject _buttonPrefab;

    [SerializeField, Tooltip("������������� �����")]
    private Toggle _soundToggle;

    [SerializeField, Tooltip("��������� ��������� �����")]
    private Slider _soundSlider;

    private List<GameObject> _buttons = new();

    private void OnEnable() => YandexGame.GetDataEvent += LoadSettings;

    private void OnDisable() => YandexGame.GetDataEvent -= LoadSettings;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            LoadSettings();
    }

    private void LoadSettings()
    {
        _soundToggle.isOn = YandexGame.savesData.MuteSound;
        _soundSlider.value = YandexGame.savesData.VolumeSound;
    }

    public void MuteAudio(bool switcher)
    {
        if (switcher)
        {
            _soundSlider.value = 0f;
        }
        else
        {
            _soundSlider.value = 1f;
        }
        YandexGame.savesData.MuteSound = switcher;
    }

    public void ChengeVolumeSound(float volume)
    {
        if (YandexGame.SDKEnabled)
        {
            var isMute = volume == 0f;
            YandexGame.savesData.MuteSound = isMute;
            YandexGame.savesData.VolumeSound = volume;
            _soundToggle.isOn = isMute;
            _soundSlider.value = volume;
            AudioListener.volume = volume;
        }
    }

    public void SaveSettings()
    {
        YandexGame.SaveProgress();
    }

    public void ResetSave()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }

    public void Play()
    {
        var levels = YandexSavesManager.GetLevelsProgress();

        if (_buttons.Count > 0)
            ClearButtonsList();

        // ������� 0 ������(����)
        int i = 1;
        bool lastComplit = true;

        //�������� ������ � ���� ��� ������ �������
        foreach (var level in levels.Skip(1))
        {
            InitNewButton(i++, lastComplit, level);
            lastComplit = level.LevelCompleted;
        }
    }
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    //�������� ������ � ���� (�����, ����������, ������ ������ ��� ������)
    private void InitNewButton(int i, bool interactable, LevelProgress level)
    {
        var levelButton = Instantiate(_buttonPrefab);
        levelButton.transform.SetParent(_buttonsGrid.transform);
        levelButton.transform.localScale = Vector3.one;
        levelButton.name += i;

        var button = levelButton.GetComponent<Button>();
        button.interactable = interactable;
        button.onClick.AddListener(() => LoadLevel(i));

        var text = levelButton.GetComponentInChildren<TextMeshProUGUI>();
        text.text = i.ToString();

        var ItemImages = levelButton.GetComponentsInChildren<Image>();

        ChangeItemColor(level.CollectItems, ItemImages);
        _buttons.Add(levelButton);
    }

    //�������� ��������� ������ �� �������� �������
    private void ChangeItemColor(int collectItems, Image[] item)
    {
        for (int i = collectItems; i > 0; i--)
        {
            item[i].color = Color.white;
        }
    }

    private void ClearButtonsList()
    {
        foreach (var button in _buttons)
        {
            Destroy(button);
        }
        _buttons.Clear();
    }

}