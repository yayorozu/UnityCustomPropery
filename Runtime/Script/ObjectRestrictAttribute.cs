using System;
using UnityEngine;

namespace Yorozu.CustomProperty
{
    public enum TargetType
    {
        /// <summary>
        /// 子供のオブジェクトのみ
        /// </summary>
        ChildOnly,
        /// <summary>
        /// Hierarchyのオブジェクトのみ
        /// </summary>
        InHierarchy,
        /// <summary>
        /// Project リソースのみ
        /// </summary>
        InProject,
    }
 
    [AttributeUsage(AttributeTargets.Field)]
    public class ObjectRestrictAttribute : PropertyAttribute
    {
        public TargetType target { get; }
 
        public ObjectRestrictAttribute(TargetType type)
        {
            target = type;
        }
    }
}