using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class HandUDP : MonoBehaviour
{
    public GameObject[] roots;
    public string       destinationAdress;
    public int          destinationPort;

    UdpClient udpClient;

    List<GameObject> list;
    public void GetAllChilds(GameObject Go, List<GameObject> list)
    {
        list.Add(Go);
        //Debug.Log(Go.name);
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            GameObject curGO = Go.transform.GetChild(i).gameObject;
            GetAllChilds(curGO, list);
        }
    }

    void Start()
    {
        list = new List<GameObject>();
        foreach(GameObject root in roots)
        {
            GetAllChilds(root, list);
        }
        udpClient = new UdpClient();
    }

    void Update()
    {
        List<string> values = new List<string>();
        foreach (GameObject go in list)
        {
            Vector3 position = go.transform.position;
            Quaternion rotation = go.transform.rotation;
            values.Add(go.name);
            values.Add(position.x.ToString());
            values.Add(position.y.ToString());
            values.Add(position.z.ToString());
            values.Add(rotation.x.ToString());
            values.Add(rotation.y.ToString());
            values.Add(rotation.z.ToString());
            values.Add(rotation.w.ToString());
        }
        string finalString = string.Join("|", values);
        byte[] sendBytes = Encoding.UTF8.GetBytes(finalString);
        //byte[] sendBytes = Encoding.ASCII.GetBytes(finalString);
        udpClient.Send(sendBytes, sendBytes.Length, destinationAdress, destinationPort);
    }
}
