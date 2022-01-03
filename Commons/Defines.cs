
namespace Popup.Defines
{
    public enum GUIPosition
    {
        LeftBottom,
        LeftMid,
        LeftTop,
        MidBottom,
        Center,
        MidTop,
        RightBottom,
        RightMid,
        RightTop,
    }

    public enum Prefab
    {
        CustomButton,
        TextMesh,
        ItemSlot,
        StackableItem,
        SolidItem,
        //Spell,
        //Buff,
        //Charactor,
        //Inventory,
        //Squad,
    }

    public enum Elements
    {
        none,
        light,  // effective sight (not good)
        dark,   // effective sight (not good)
        fire,   // effective touch (not good)
        water,  // effective taste (not good)
        air,    // effective smell (not good)
        earth,  // effective hearing (not good)
    }

    public enum SpellEffective
    {
        none,
        hearing,
        sight,
        taste,
        smell,
        touch,
    }

    public enum ItemCat
    {
        solid       = 0x0001,
        stackable   = 0x0002,
    }

    //public enum EquipSlot
    //{
    //    none,
    //    head,
    //    chest,
    //    leggings,
    //    boots,
    //    gloves,
    //    amulet,
    //    leftRing,
    //    rightRing,
    //    leftHand,
    //    rightHand,
    //}

    public enum Grade
    {
        none,
        white,
        green,
        blue,
        purple,
        orange,
        red,
    }

    public enum EffectType
    {
        push,
        pull,
    }

    public enum DefenceType
    {
        evade,
        parry,
        block,
    }

    public enum DamageType
    {
        smite,
        pierce,
        cut,
        scratch,
    }

    public enum DotType
    {
        poison,
        bleeding,
        disease,
        stun,
        sleep,
    }

    public enum MindType
    {
        unfocus,
        fear,
        scared,
        none,
        brave,
        berserk,
        focus,
    }

    public enum SceneType
    {
        entrance,
        lobby,
        game,
    }
}
