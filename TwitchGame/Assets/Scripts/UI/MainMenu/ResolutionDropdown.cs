using TMPro;
using UnityEngine;

public class ResolutionDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdownMenu;

    private Resolution[] _resolutions;

    public void InitOptions()
    {
        _resolutions = Screen.resolutions;
    
        // we add the screen resolutions to the dropdown
        for (int i = 0; i < _resolutions.Length; i++)
        {
            var option = new TMPro.TMP_Dropdown.OptionData(ResToString(_resolutions[i]));
            _dropdownMenu.options.Add(option);
        }
    }

    // used for the label
    private string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }

    public void ResolutionChanged()
    {
        // PlayerPrefs var changed in the OptionsMenu script
        Screen.SetResolution(_resolutions[_dropdownMenu.value].width, _resolutions[_dropdownMenu.value].height, Screen.fullScreenMode);
    }
}
