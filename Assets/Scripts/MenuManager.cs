using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] colors;
    public TextMeshProUGUI levelText;
    public TMP_InputField username;

    private int level;
    private int minLevel = 1;
    private int maxLevel = 10;

    private int colorSelected;

    private void Start()
    {
        level = int.Parse(levelText.text);
        LoadUserOptions();
    }

    private void Update()
    {
        ColorSelection();
    }

    public void SaveUserOptions()
    {
        // Persistencia de datos entre escenas
        DataPersistence.sharedInstance.colorSelected = colorSelected;
        DataPersistence.sharedInstance.color = colors[colorSelected].GetComponent<Image>().color;
        
        DataPersistence.sharedInstance.level = level;
        
        DataPersistence.sharedInstance.username = username.text;
        
        // Persistencia de datos entre partidas
        DataPersistence.sharedInstance.SaveForFutureGames();
    }

    public void LoadUserOptions()
    {
        // Tal y como lo hemos configurado, si tiene esta clave, entonces tiene todas
        if (PlayerPrefs.HasKey("COLOR_SELECTED"))
        {
            colorSelected = PlayerPrefs.GetInt("COLOR_SELECTED");
            
            level = PlayerPrefs.GetInt("LEVEL");
            UpdateLevel();

            username.text = PlayerPrefs.GetString("USERNAME");
        }
    }

    #region Level Settings

    public void PlusLevel()
    {
        level++;
        level = Mathf.Clamp(level, minLevel, maxLevel);
        UpdateLevel();
    }

    public void MinusLevel()
    {
        level--;
        level = Mathf.Clamp(level, minLevel, maxLevel);
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        levelText.text = level.ToString();
    }
    #endregion

    #region Color Settings

    private void ColorSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            colorSelected++;
        }else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            colorSelected--;
        }

        colorSelected %= 3;
        ChangeColorSelection();
    }

    private void ChangeColorSelection()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i].transform.GetChild(0).gameObject.SetActive(i == colorSelected);
        }
    }

    #endregion
}
