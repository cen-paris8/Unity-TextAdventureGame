using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputAction/Examine")]
public class Examine : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputwords)
    {
        controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun(controller.interactableItems.examineDictionary, separatedInputwords[0], separatedInputwords[1]));
    }

}
