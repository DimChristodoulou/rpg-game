using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreationManager : MonoBehaviour
{
    private GameObject rotateClockwise;
    private GameObject rotateAntiClockwise;
    
    private GameObject classNameText;
    private GameObject classDescriptionText;

    private GameObject raceNameText;
    private GameObject raceDescriptionText;

    private GameObject raceHuman3DGameObject;
    private GameObject raceElf3DGameObject;
    private GameObject raceOrc3DGameObject;
    private GameObject raceDwarf3DGameObject;
    
    public string characterChosenClass;
    public string characterChosenRace;
    public string characterName;
    
    // Start is called before the first frame update
    void Start()
    {
        rotateClockwise = GameObject.Find("rotateClockwiseBtn");
        rotateAntiClockwise = GameObject.Find("rotateAntiClockwiseBtn");
        
        raceElf3DGameObject = GameObject.Find("class_templar_undead");
        raceOrc3DGameObject = GameObject.Find("class_specialist_vampire");
        raceDwarf3DGameObject = GameObject.Find("class_druid_ghoul");
        raceHuman3DGameObject = GameObject.Find("class_mageblade_lizard_warrior");

        setAll3DModelsToInactive();

        classNameText = GameObject.Find("classText");
        classDescriptionText = GameObject.Find("classDescriptionText");
        raceNameText = GameObject.Find("raceText");
        raceDescriptionText = GameObject.Find("raceDescriptionText");
        
        classNameText.SetActive(false);
        classDescriptionText.SetActive(false);
        raceNameText.SetActive(false);
        raceDescriptionText.SetActive(false);
    }

    public void setAll3DModelsToInactive(){
        raceHuman3DGameObject.SetActive(false);
        raceElf3DGameObject.SetActive(false);
        raceOrc3DGameObject.SetActive(false);
        raceDwarf3DGameObject.SetActive(false);
    }
    
    public void selectRace(string chosenRace){
        setAll3DModelsToInactive();
        switch (chosenRace)
        {
            case "human":
                characterChosenRace = globals.RaceHuman;
                raceNameText.GetComponent<TextMeshProUGUI>().text = Localization.RaceHuman;
                raceDescriptionText.GetComponent<TextMeshProUGUI>().text = Localization.RaceHumanDescription;
                raceHuman3DGameObject.SetActive(true);
                break;
            case "elf":
                characterChosenRace = globals.RaceElf;
                raceNameText.GetComponent<TextMeshProUGUI>().text = Localization.RaceElf;
                raceDescriptionText.GetComponent<TextMeshProUGUI>().text = Localization.RaceElfDescription;
                raceElf3DGameObject.SetActive(true);
                break;
            case "dwarf":
                characterChosenRace = globals.RaceDwarf;
                raceNameText.GetComponent<TextMeshProUGUI>().text = Localization.RaceDwarf;
                raceDescriptionText.GetComponent<TextMeshProUGUI>().text = Localization.RaceDwarfDescription;
                raceDwarf3DGameObject.SetActive(true);
                break;
            case "orc":
                characterChosenRace = globals.RaceOrc;
                raceNameText.GetComponent<TextMeshProUGUI>().text = Localization.RaceOrc;
                raceDescriptionText.GetComponent<TextMeshProUGUI>().text = Localization.RaceOrcDescription;
                raceOrc3DGameObject.SetActive(true);
                break;
        }
        raceNameText.SetActive(true);
        raceDescriptionText.SetActive(true);
    }
    
    public void selectClass(string chosenClass){
        switch (chosenClass)
        {
            case "fighter":
                characterChosenClass = globals.ClassFighter;
                classNameText.GetComponent<TextMeshProUGUI>().text = Localization.ClassFighter;
                classDescriptionText.GetComponent<TextMeshProUGUI>().text = Localization.ClassFighterDescription;
                break;
            case "mageblade":
                characterChosenClass = globals.ClassMageBlade;
                classNameText.GetComponent<TextMeshProUGUI>().text = Localization.ClassMageBlade;
                classDescriptionText.GetComponent<TextMeshProUGUI>().text = Localization.ClassMageBladeDescription;
                break;
            case "specialist":
                characterChosenClass = globals.ClassSpecialist;
                classNameText.GetComponent<TextMeshProUGUI>().text = Localization.ClassSpecialist;
                classDescriptionText.GetComponent<TextMeshProUGUI>().text = Localization.ClassSpecialistDescription;
                break;
            case "templar":
                characterChosenClass = globals.ClassTemplar;
                classNameText.GetComponent<TextMeshProUGUI>().text = Localization.ClassTemplar;
                classDescriptionText.GetComponent<TextMeshProUGUI>().text = Localization.ClassTemplarDescription;
                break;
            case "druid":
                characterChosenClass = globals.ClassDruid;
                classNameText.GetComponent<TextMeshProUGUI>().text = Localization.ClassDruid;
                classDescriptionText.GetComponent<TextMeshProUGUI>().text = Localization.ClassDruidDescription;
                break;
            case "sorcerer":
                characterChosenClass = globals.ClassSorcerer;
                classNameText.GetComponent<TextMeshProUGUI>().text = Localization.ClassSorcerer;
                classDescriptionText.GetComponent<TextMeshProUGUI>().text = Localization.ClassSorcererDescription;
                break;
        }
        classNameText.SetActive(true);
        classDescriptionText.SetActive(true);
    }

    public void finalizeCharacterCreation(){
        PlayerPrefs.SetString("characterName", characterName);
        PlayerPrefs.SetString("characterRace", characterChosenRace);
        PlayerPrefs.SetString("characterClass", characterChosenClass);
        SceneManager.LoadScene("mainScene");
    }
    
    public void updateCharacterName(InputField characterInputName){
        characterName = characterInputName.text;
    }
}
