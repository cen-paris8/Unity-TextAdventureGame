using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObject> usableItemList;

    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();

    [HideInInspector] public List<string> nounsInRoom = new List<string>();

    Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();

    List<string> nounsInInventory = new List<string>();
    GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public string GetObjectNotInInventory(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];

        if (!nounsInInventory.Contains(interactableInRoom.noun))
        {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.description;
        }

        return null;
    }

    public void AddActionResponsesToUseDictionary()
    {
        Debug.Log(message: $" AddActionResponsesToUseDictionary() ");
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            string noun = nounsInInventory[i];

            Debug.Log(message: $" noun: {noun} ");
            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsableList(noun);

            Debug.Log(message: $" interactableObjectInInventory: {interactableObjectInInventory} ");
            if (interactableObjectInInventory == null)
                continue;

            for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
            {
                Debug.Log(message: $"useDictionary.ContainsKey(noun) : {useDictionary.ContainsKey(noun)} ");
                Interaction interaction = interactableObjectInInventory.interactions[j];
                Debug.Log(message: $"interaction.actionResponse: {interaction.actionResponse} ");
                if (interaction.actionResponse == null)
                    continue;

                if (!useDictionary.ContainsKey(noun))
                {
                    useDictionary.Add(noun, interaction.actionResponse);
                }
            }
        }
    }

    InteractableObject GetInteractableObjectFromUsableList(string noun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].noun == noun)
            {
                return usableItemList[i];
            }
        }
        return null;
    }

    public void DisplayInventory()
    {
        controller.LogStringWithReturn("You look in your backpack, inside you have: ");

        for(int i = 0; i < nounsInInventory.Count; i++)
        {
            controller.LogStringWithReturn(nounsInInventory[i]);
        }
    }

    public void ClearCollections()
    {
        examineDictionary.Clear();
        takeDictionary.Clear();
        nounsInRoom.Clear();
    }

    public Dictionary<string, string> Take(string[] separatedInputWords)
    {
        string noun = separatedInputWords[1];

        Debug.Log(message: $" Take() noun : {noun}  ");
        if (nounsInRoom.Contains(noun))
        {
            Debug.Log(message: $" Take() nounsInRoom.Contains(noun) : {nounsInRoom.Contains(noun)}  ");
            nounsInInventory.Add(noun);
            AddActionResponsesToUseDictionary();
            nounsInRoom.Remove(noun);
            return takeDictionary;
        }
        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to take.");
            return null;
        }

    }

    public void UseItem(string[] separatedInputWords)
    {
        Debug.Log(message: $"UseItem ");
        string nounToUse = separatedInputWords[1];
        Debug.Log(message: $"nounToUse : {nounToUse} useDictionary.ContainsKey(nounToUse) : {useDictionary.ContainsKey(nounToUse)} ");
        if (nounsInInventory.Contains(nounToUse)){
            if (useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                if (!actionResult)
                {
                    controller.LogStringWithReturn("Humm. Nothing happens");
                }
            }
            else
            {
                controller.LogStringWithReturn("You can't use the " + nounToUse);
            }
        }
        else
        {
            controller.LogStringWithReturn("There is no " + nounToUse + " in your inventory to use");
        }
    }

}
