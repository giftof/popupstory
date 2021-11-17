using UnityEngine;
using System.Collections;
using Popup.Configs;
using Popup.Defines;
using Popup.Library;
using Popup.Framework;
using Newtonsoft.Json;

using Popup.ServerJob;


public class Spell : IPopupObject
{
	[JsonProperty]
	public int uid {get; protected set; }
	[JsonProperty]
	public int slotId { get; protected set; }
	[JsonProperty]
	public string name { get; protected set; }
	public SpellEffective effective {get; protected set; }
	public Elements element {get; protected set; }



	public Spell(string attribute)  // impl.
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


	// public 	int GetUID() => uid;

	public bool IsExist => false; // impl.

	public object Duplicate() => MemberwiseClone();
	public object DuplicateNew() => ((Spell)Duplicate()).uid = ServerJob.RequestNewUID;
}


public class Buff
{
	int duration;
	int effectValue;
}
