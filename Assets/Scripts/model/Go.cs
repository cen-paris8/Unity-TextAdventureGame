using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/InputAction/Go")]
public class Go : InputAction
{
    public override void RespondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length < 2)
        {
            Debug.Log(message: $"Wrong choice");
            controller.roomNavigation.AttemptToChangeRooms("");
        }
        Debug.Log(message: $"separatedInputwords[1] : {separatedInputWords[1]}");
        Debug.Log(message: $"separatedInputwords[0] : {separatedInputWords[0]}");
        controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
    }
}
