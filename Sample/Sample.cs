﻿using UnityEngine;
using UnityEngine.UI;

namespace Yorozu.Sample
{
	using CustomProperty;

	public enum Test
	{
		A,
		AB,
		ABC,
		BCD,
	}
	
	public class Sample : MonoBehaviour
	{
		[SerializeField, AddComponent]
		private SpriteRenderer _spriteRenderer;

		[SerializeField, GetComponent]
		private Image _image;

		[SerializeField, SearchableEnum]
		private Test _test;

		[SerializeField, PlayCache]
		private int _cacheValue;

		[SerializeField]
		private Color _color;

		[SerializeField, Preview]
		private GameObject _preview;
	}
}