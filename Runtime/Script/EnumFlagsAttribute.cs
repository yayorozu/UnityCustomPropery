using System;
using UnityEngine;

namespace Yorozu.CustomProperty
{
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
	public sealed class EnumFlagsAttribute : PropertyAttribute
	{
		public readonly bool IsButton;
		
		public EnumFlagsAttribute(bool isButton = false)
		{
			IsButton = isButton;
		}
	}
}