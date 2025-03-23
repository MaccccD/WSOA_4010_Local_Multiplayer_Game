using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIndexManager : MonoBehaviour
{
    public static PlayerIndexManager instance;

    private Dictionary<string, int> deviceToPlayerIndex = new Dictionary<string, int>();
    private int nextPlayerIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PersistentPlayerIndexManager created and will persist across scenes.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int AssignPlayerIndex(PlayerInput playerInput)
    {
        if (playerInput.devices.Count == 0)
        {
            Debug.LogError("PlayerInput has no devices!");
            return -1;
        }

        string key = playerInput.devices[0].deviceId.ToString();
        Debug.Log($"Assigning index for device ID: {key}");

        if (deviceToPlayerIndex.ContainsKey(key))
        {
            int existingIndex = deviceToPlayerIndex[key];
            Debug.Log($"Device {key} already has index {existingIndex}.");
            return existingIndex;
        }
        else
        {
            int assignedIndex = nextPlayerIndex;
            deviceToPlayerIndex.Add(key, assignedIndex);
            nextPlayerIndex++;
            Debug.Log($"Assigned new index {assignedIndex} to device {key}.");
            return assignedIndex;
        }
    }

    public int GetPlayerCount()
    {
        return deviceToPlayerIndex.Count;
    }
}
