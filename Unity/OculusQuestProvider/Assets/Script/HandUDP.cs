using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class HandUDP : MonoBehaviour
{
    public GameObject   root;
    public string       destinationAdress;
    public int          destinationPort;
    
    UdpClient udpClient;
    List<Transform> list;

    string[] boneNames = {  "b_l_wrist", "b_l_thumb1", "b_l_thumb2", "b_l_thumb3", "b_l_index1", "b_l_index2", "b_l_index3", "b_l_middle1", "b_l_middle2", "b_l_middle3", "b_l_ring1", "b_l_ring2", "b_l_ring3", "b_l_pinky0", "b_l_pinky1", "b_l_pinky2", "b_l_pinky3",
                            "b_r_wrist", "b_r_thumb1", "b_r_thumb2", "b_r_thumb3", "b_r_index1", "b_r_index2", "b_r_index3", "b_r_middle1", "b_r_middle2", "b_r_middle3", "b_r_ring1", "b_r_ring2", "b_r_ring3", "b_r_pinky0", "b_r_pinky1", "b_r_pinky2", "b_r_pinky3",
                            "CenterEyeAnchor"};

    Transform RecursiveFind(Transform transform, string name)
    {
        foreach(Transform childTrans in transform)
        {
            if(childTrans.name == name)
            {
                return childTrans;
            }
            else
            {
                Transform found = RecursiveFind(childTrans, name);
                if(found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }
    
    public void GetAllChilds(GameObject Go, List<Transform> list)
    {
        Debug.Log(Go.name);
        foreach (string boneName in boneNames)
        {
            Transform found = RecursiveFind(Go.transform, boneName);
            if (found != null)
            {
                list.Add(found);
            }
            else
            {
                Debug.Log(Go.name + " not found!");
            }
        }
    }

    void Start()
    {
        Debug.Log("starting the shit");
        list = new List<Transform>();
        GetAllChilds(root, list);
        udpClient = new UdpClient();
    }

    void Update()
    {
        List<string> values = new List<string>();
        foreach (Transform tr in list)
        {
            Vector3 position = tr.position;
            Quaternion rotation = tr.rotation;
            values.Add(tr.name);
            values.Add(position.x.ToString());
            values.Add(position.y.ToString());
            values.Add(position.z.ToString());
            values.Add(rotation.x.ToString());
            values.Add(rotation.y.ToString());
            values.Add(rotation.z.ToString());
            values.Add(rotation.w.ToString());
        }
        string finalString = string.Join("|", values);
        Debug.Log(finalString);
        byte[] sendBytes = Encoding.UTF8.GetBytes(finalString);
        udpClient.Send(sendBytes, sendBytes.Length, destinationAdress, destinationPort);
    }
}
