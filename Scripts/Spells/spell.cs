using UnityEngine;
using System.Collections;
using Popup.Configs;
using Popup.Defines;
using Popup.Library;
using Popup.Framework;
using Popup.Charactors;
using Newtonsoft.Json;



public class Spell : PopupObject
{
	[JsonProperty]
	public SpellEffective Effective {get; protected set; }
	[JsonProperty]
	public Elements Element {get; protected set; }
	[JsonProperty]
	public int Affect { get; protected set; }



	public Spell(string attribute)  // impl.
	{
		if (string.IsNullOrEmpty(attribute))
		{
			Name 		= "null";
			Uid 		= Manager.Instance.network.REQ_NEW_ID();
			Effective 	= SpellEffective.none;
			Element 	= Elements.none;
		}
		Name 		= "null";
		Uid 		= Manager.Instance.network.REQ_NEW_ID();
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

	public override bool IsExist => false; // impl.

	public override object DeepCopy(int uid) => MemberwiseClone(); // impl.
	// public object DuplicateNew() => ((Spell)Duplicate()).uid = ServerJob.RequestNewUID;
}


public class Buff
{
	int duration;
	int effectValue;
}
