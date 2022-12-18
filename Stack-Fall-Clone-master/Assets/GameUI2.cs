using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI2 : MonoBehaviour
{
    public Image levelSlider;
    public Image currentLevelImg;
    public Image nextLevelImg;

    public Material playerMat;

    public GameObject settingBTN;
    public GameObject allBTN;

    public GameObject SoundONBTN ;
    public GameObject SoundOFFBTN;


    private PlayerController2 player;

    public bool buttonSettingBO;        

    [SerializeField] private GameObject homeUI;
    [SerializeField] private GameObject gameUI;

    void Start()
    {
        playerMat = FindObjectOfType<PlayerController2>().transform.GetChild(1).GetComponent<MeshRenderer>().material;
        player =FindObjectOfType<PlayerController2>();

        levelSlider.transform.GetComponent<Image>().color = playerMat.color + Color.gray;

        levelSlider.color = playerMat.color;

        currentLevelImg.color = playerMat.color;    

        nextLevelImg.color = playerMat.color;

        SoundONBTN.GetComponent<Button>().onClick.AddListener((() => SoundManager2.instance.soundOnOff()));   
        SoundOFFBTN.GetComponent<Button>().onClick.AddListener((() => SoundManager2.instance.soundOnOff()));

        //PlayerPrefs.DeleteAll();
        
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !ignoreUI() && player.playerstate == PlayerController2.PlayerState.Prepare)
        {
            player.playerstate = PlayerController2.PlayerState.Playing;
            homeUI.SetActive(false);
            gameUI.SetActive(true);
            
        }


        if (SoundManager2.instance.sound)
        {
            SoundONBTN.SetActive(true);
            SoundOFFBTN.SetActive(false);
        }
        else
        {
            SoundONBTN.SetActive(false);
            SoundOFFBTN.SetActive(true);   
        }

    }

    private bool ignoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position= Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for (int i = 0; i < raycastResults.Count; i++)
        {
            if(raycastResults[i].gameObject.GetComponent<IgnoreGameUI2>() != null)
            {
                raycastResults.RemoveAt(i);
                i--;
            }

        }
        Debug.Log("--->>" + raycastResults.Count);

        return raycastResults.Count > 0;
    }

    public  void levelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;    
    }

    public void settingShow()
    {
        buttonSettingBO = !buttonSettingBO;
        allBTN.SetActive(buttonSettingBO);  
    }
}
