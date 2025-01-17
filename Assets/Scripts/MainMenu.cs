using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuBack;
    public GameObject Story;
    public GameObject Setting;

    public GameObject CheckMusic;
    public GameObject CheckSound;

    public Slider SliderMusic;
    public Slider SliderSound;

    public int isMusic = 0;
    public int isSound = 0;

    public float musicVolume = 1;
    public float soundVolume = 1;

    private void Start()
    {
        CheckMusic.SetActive(true);
        CheckSound.SetActive(true);

        SliderMusic.onValueChanged.AddListener(delegate { MusicValueChange(); }); //delegate = 대리자(놀이공원에 대신 줄서주는 느낌 AddListener(들을준비)가 되면 함수 실행(여러개 줄 설 수있음)
        SliderSound.onValueChanged.AddListener(delegate { SoundValueChange(); });

        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", SliderMusic.value);
        }
        SliderMusic.value = musicVolume;
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("SoundVolume", SliderSound.value);
        }
        SliderSound.value = soundVolume;

    }

    void MusicValueChange()
    {
        Debug.Log(SliderMusic.value);
        PlayerPrefs.SetFloat("MusicVolume", SliderMusic.value);
    }

    void SoundValueChange()
    {
        Debug.Log(SliderSound.value);
        PlayerPrefs.SetFloat("SoundVolume", SliderSound.value);
    }


    void OpenStory()
    {
        Story.SetActive(true);
        Story.GetComponent<Animator>().SetTrigger("Open");
    }
    
    void OpenSetting()
    {
        Setting.SetActive(true);
        Setting.GetComponent<Animator>().SetTrigger("Open");
    }

    void OpenMenuBack()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Open");
    }

    public void BtnBack(int num)
    {
        switch (num)
        {
            case 0://MENUAL
                Story.GetComponent<Animator>().SetTrigger("Close");
                Invoke("OpenMenuBack", 1.5f);
                break;
            case 1://Story
                Setting.GetComponent<Animator>().SetTrigger("Close");
                Invoke("OpenMenuBack", 1.5f);
                break;

        }
    }

    public void BtnMusic()
    {
        CheckMusic.SetActive(!CheckMusic.activeInHierarchy);
    }

    public void BtnSound()
    {
        CheckSound.SetActive(!CheckSound.activeInHierarchy);
    }

    public void BtnStory()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenStory", 1.5f);
    }

    public void BtnSetting()
    {
        MenuBack.GetComponent<Animator>().SetTrigger("Close");
        Invoke("OpenSetting", 1.5f);
    }

}
