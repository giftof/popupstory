using UnityEngine;
using System.Collections;
using Popup.Configs;
using Popup.Defines;
using Popup.Library;
using Popup.Framework;
using Popup.Charactors;
using Newtonsoft.Json;



public class Spell : IPopupObject, ISpell
{
	[JsonProperty]
	public int uid {get; protected set; }
	[JsonProperty]
	public int SlotId { get; protected set; }
	[JsonProperty]
	public string Name { get; protected set; }
	[JsonProperty]
	public SpellEffective Effective {get; protected set; }
	[JsonProperty]
	public Elements Element {get; protected set; }
	[JsonProperty]
	public int Affect { get; protected set; }
	[JsonProperty]
	public GameObject Owner { get; set; }



	public Spell(string attribute)  // impl.
	{
		if (string.IsNullOrEmpty(attribute))
		{
			Name 		= "null";
			uid 		= Manager.Instance.network.RequestNewUID();
			Effective 	= SpellEffective.none;
			Element 	= Elements.none;
		}
		Name 		= "null";
		uid 		= Manager.Instance.network.RequestNewUID();
		Effective 	= SpellEffective.none;
		Element 	= Elements.none;

	}


	public int AffectiveValue(Charactor target)
    {
		switch (Effective)
        {
			case SpellEffective.hearing:
				return 0;
			case SpellEffective.sight:
				return 0;
			case SpellEffective.smell:
				return 0;
			case SpellEffective.taste:
				return 0;
			case SpellEffective.touch:
				return 0;
			default:
				return 0;
        }
    }

	public bool IsExist => false; // impl.

	public object DeepCopy(int? uid = null, int? charge = null) => MemberwiseClone(); // impl.
	// public object DuplicateNew() => ((Spell)Duplicate()).uid = ServerJob.RequestNewUID;
}


public class Buff
{
	int duration;
	int effectValue;
}
