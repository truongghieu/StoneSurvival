using System;
using UnityEngine;

namespace HPoolingObject
{
public interface IObject
{

    GameObject gameObject { get; }
    public string UniqueID { get; set; }

    public void Init(Action<IObject> returnToPool);
    public void returnToPool();
}
}