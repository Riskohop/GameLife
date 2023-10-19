using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("ImagesButtons")] 
    public GameObject playImage;
    public GameObject pauseImage;
    public GameObject isPencilImage;
    public GameObject isEraseImage;
    [Header("Slider")]
    public Slider speedSlider;
    [Header("Rules")] 
    public TMP_InputField ruleB;
    public TMP_InputField ruleS;
}
