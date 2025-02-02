using System;
using UnityEngine;

namespace Yorozu.CustomProperty
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ValidateNotNullAttribute : PropertyAttribute
    {
    }
}