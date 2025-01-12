using UnityEngine;
using System.Collections.Generic;

public class ToastManager : MonoBehaviour
{
    public ToastMessage toastMessagePrefab;
    public float toastSpacing = 50;

    public MessageSO[] testMessages;
    public float testMessageInterval = 4;
    private int testMessageIndex = 0;

    private List<ToastMessage> messages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Shuffle(testMessages);
        messages = new List<ToastMessage>();
        InvokeRepeating("AddTestMessage", 0, testMessageInterval);
    }

    private void AddTestMessage()
    {
        MessageSO next = testMessages[testMessageIndex];
        while (!CanDisplayMessage(next))
        {
            testMessageIndex = (testMessageIndex + 1) % testMessages.Length;
            next = testMessages[testMessageIndex];
        }
        AddMessage(next);
        testMessageIndex = (testMessageIndex + 1) % testMessages.Length;
    }

    private bool CanDisplayMessage(MessageSO msg)
    {
        return !msg.boboMode || ModeHandler.Instance.boboMode;
    }

    public void AddMessage(MessageSO msg)
    {
        ToastMessage toast = Instantiate(toastMessagePrefab, transform);
        toast.Init(msg);
    }

    static void Shuffle<T>(T[] array)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < array.Length; t++)
        {
            T tmp = array[t];
            int r = Random.Range(t, array.Length);
            array[t] = array[r];
            array[r] = tmp;
        }
    }
}
