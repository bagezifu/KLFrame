using UnityEngine;
using System.Collections;
using System;


[AttributeUsage(AttributeTargets.Field|AttributeTargets.Property, AllowMultiple = true)]
public class AutoAssignAttribute : Attribute
{
    public AutoAssignAttribute() {}
}
