using UnityEngine;
using System.Collections;
using Popup.Configs;
using Popup.Defines;
using Popup.Utils;

using Popup.ServerJob;


public class Spell
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
}

public class Buff
{
	int duration;
	int effectValue;
}
