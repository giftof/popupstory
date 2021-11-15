﻿using UnityEngine;
using System.Collections;
using Popup.Configs;
using Popup.Defines;
using Popup.Library;
using Popup.Framework;

using Popup.ServerJob;


public class Spell : IPopupObject
{
	string			name;
	int				uid;
	SpellEffective	effective;
	Elements		element;



	public Spell(string attribute)  // fix
	{
		if (string.IsNullOrEmpty(attribute))
		{
			name 		= "null";
			uid 		= ServerJob.RequestNewUID;
			effective 	= SpellEffective.none;
			element 	= Elements.none;
		}
		name 		= "null";
		uid 		= ServerJob.RequestNewUID;
		effective 	= SpellEffective.none;
		element 	= Elements.none;

	}


	public 	int GetUID() => uid;


}

public class Buff
{
	int duration;
	int effectValue;
}
