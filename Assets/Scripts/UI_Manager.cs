using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Blanket comment for the mess below: So every canvas from our canvas prefab comes with a UI Manager attached. This UI manager is connected to the local versions of
    // all the relevant UI elements. When it is initialized, it takes those locals, and copies them to the static variables that hold the public versions of them, so that
    // when the game manager calls any static functions that use these, it uses the one from the most recently loaded scene
    private static GameObject popup;
    public static GameObject pauseText; // these are public so that Game Manager knows when a menu is up while another menu's button is being pressed
    public static GameObject stageText;
    public static GameObject quitText;

    private static GameObject pauseButton;
    private static UnityEngine.UI.Button pauseComponent; // we need to enable and disable  this, so we want to store the component separately
    private static GameObject stageButton;
    private static GameObject quitButton;

    private static GameObject stageConfirm;
    private static GameObject stageCancel;
    private static GameObject quitConfirm;
    private static GameObject quitCancel;

    public GameObject localPopup;
    public GameObject localPauseText;
    public GameObject localStageText;
    public GameObject localQuitText;

    public GameObject localPauseButton;
    public GameObject localStageButton;
    public GameObject localQuitButton;

    public GameObject localStageConfirm;
    public GameObject localStageCancel;
    public GameObject localQuitConfirm;
    public GameObject localQuitCancel;

    void Awake() // when entering a scene with a new UI manager, connect all local dynamic UI elements to the held ones so they can be used by Show and Hide text functions
        // does the same for buttons so they can be enabled and disabled, but accounts for the fact that not every screen has every button
        // NOTE if you have a button on the screen that has a slot here but do not assign it in the slot here, pressing that button may result in errors
    {
        popup = localPopup;
        pauseText = localPauseText;
        stageText = localStageText;
        quitText = localQuitText;

        stageConfirm = localStageConfirm;
        stageCancel = localStageCancel;
        quitConfirm = localQuitConfirm;
        quitCancel = localQuitCancel;

        if (localPauseButton  != null)
        {
            pauseButton = localPauseButton;
            pauseComponent = pauseButton.GetComponent<UnityEngine.UI.Button>();
        }
        if (localStageButton != null)
        {
            stageButton = localStageButton;
        }
        if (localQuitButton != null)
        {
            quitButton = localQuitButton;
        }

        popup.SetActive(false);
        pauseText.SetActive(false);
        stageText.SetActive(false);
        quitText.SetActive(false);

        stageConfirm.SetActive(false);
        stageCancel.SetActive(false);
        quitConfirm.SetActive(false);
        quitCancel.SetActive(false);
    }

    public static void ShowPauseText() // activates the currently held popup and pauseText
    {
        popup.SetActive(true);
        pauseText.SetActive(true);
    }
    public static void HidePauseText() // activates the currently held popup and pauseText
    {
        popup.SetActive(false);
        pauseText.SetActive(false);
    }

    public static void ShowStageText()
    {
        popup.SetActive(true);
        stageText.SetActive(true);

        if (pauseButton != null) { pauseComponent.interactable = false; }

        stageConfirm.SetActive(true);
        stageCancel.SetActive(true);
    }
    public static void HideStageText()
    {
        popup.SetActive(false);
        stageText.SetActive(false);
        stageConfirm.SetActive(false);
        stageCancel.SetActive(false);

        if (DoesPauseButtonExist()) { EnablePauseButton(); } // pause button will be deactivated for the stage screen to be up,
                                                             // so we need to reactivate it
    }

    public static void ShowQuitText()
    {
        popup.SetActive(true);
        quitText.SetActive(true);

        if (pauseButton != null) { pauseComponent.interactable = false; }

        quitConfirm.SetActive(true);
        quitCancel.SetActive(true);
    }
    public static void HideQuitText()
    {
        popup.SetActive(false);
        quitText.SetActive(false);
        quitConfirm.SetActive(false);
        quitCancel.SetActive(false);

        if (DoesPauseButtonExist()) { EnablePauseButton(); } // pause button will be deactivated for the quit screen to be up,
                                                                                 // so we need to reactivate it
    }

    public static bool DoesPauseButtonExist() // read accessor for pause button to check if it is null
    {
        if (pauseButton != null)
        {
            return true;
        } else
        {
            return false;
        }
    }
    public static void EnablePauseButton() // write accessor for pause button to enable it
    {
        pauseComponent.interactable = true;
    }
}
