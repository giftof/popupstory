using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network : MonoBehaviour
{
    public int RequestNewUID() => Manager.Instance.server.RequestNewUID;
}
