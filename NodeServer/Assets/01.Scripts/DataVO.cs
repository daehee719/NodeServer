using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public int id;
    public string name;
}
[Serializable]
public class DataVO
{
    public string name;
    public List<User> users;
}
