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
        AddMessage(testMessages[testMessageIndex]);
        testMessageIndex = (testMessageIndex + 1) % testMessages.Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*float offset = 0;
        for (int i = messages.Count - 1; i >= 0; i--)
        {
            var toast = messages[i];
            toast.lifeTime -= Time.deltaTime;
            if (toast.lifeTime <= 0)
            {
                messages.RemoveAt(i);
                Destroy(toast.gameObject);
                offset += toastSpacing;
            }
        }*/
        /*if (offset > 0)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                var toast = messages[i];
                var pos = toast.transform.position;
                pos.y -= offset;
                toast.transform.position = pos;
            }
        }*/
    }

    public void AddMessage(MessageSO msg)
    {
        ToastMessage toast = Instantiate(toastMessagePrefab, transform);
        /*var pos = toast.transform.position;
        pos.y -= messages.Count * toastSpacing;
        toast.transform.position = pos;*/
        toast.Init(msg);
        //messages.Add(toast);
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
