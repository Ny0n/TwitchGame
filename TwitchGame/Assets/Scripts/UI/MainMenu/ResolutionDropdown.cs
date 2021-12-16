using UnityEngine;

public class ResolutionDropdown : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdownMenu;

    private Resolution[] _resolutions;
    
    void Awake()
    {
        _resolutions = Screen.resolutions;
        
        // we add the screen resolutions to the dropdown
        for (int i = 0; i < _resolutions.Length; i++)
        {
            var option = new TMPro.TMP_Dropdown.OptionData(ResToString(_resolutions[i]));
            dropdownMenu.options.Add(option);
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
        Screen.SetResolution(_resolutions[dropdownMenu.value].width, _resolutions[dropdownMenu.value].height, false);
    }
}
