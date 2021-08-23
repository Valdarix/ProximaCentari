using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Text _volumeValue;
    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private Slider _ambientSlider;
    [SerializeField] private Text _ambientValue;

    [SerializeField] private Text _brigthnessValue;
    [SerializeField] private Slider _brigthnessSlider;

    private PostProcessVolume _postProcess;
    private ColorGrading _colorGrading = null;
    private float _brightnessLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        _volumeSlider.value = AudioManager.Instance._audioSource.volume;
        _ambientSlider.value = AudioManager.Instance._ambientSource.volume;
        

        _postProcess = GameObject.FindWithTag("PostProcess").GetComponent<PostProcessVolume>();
        if (_postProcess == null)
        {
            Debug.LogError("PostProcessor is missing");
        }
        else
        {
            _postProcess.profile.TryGetSettings(out _colorGrading);
            _brigthnessSlider.value = _colorGrading.brightness.value;
        }
    }
    
    public void OnVolumeAdjust()
    {
        var volumeCalc = (int)(_volumeSlider.value * 100);
        _volumeValue.text = volumeCalc + "%";
        AudioManager.Instance._audioSource.volume = _volumeSlider.value;
    }

    public void OnAmbientVolumeAdjust()
    {
        var volumeCalc = (int)(_ambientSlider.value * 100);
        _ambientValue.text = volumeCalc + "%";
        AudioManager.Instance._ambientSource.volume = _ambientSlider.value;
    }
    
    public void AdjustAmbientLight ()
    {
        _brigthnessValue.text = _brigthnessSlider.value + "%";
        _colorGrading.brightness.value = _brigthnessSlider.value;
    }

    public void BackButtonClick()
    {
        SceneController.Instance.LoadGameScene("MainMenu");
    }
}
