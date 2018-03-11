using model.Attributes;

namespace model.Enums
{
    //Go to AttibuteUtils for conversions Code -> SlotEnum

    public enum SlotEnum
    {
        [Code("undefined")]
        UNDEFINED = 0,
        [Code("A-Main")]
        Team_A_Main = 1,
        [Code("A-Sub")]
        Team_A_Subs = 2,
        [Code("B-Main")]
        Team_B_Main = 3,
        [Code("B-Sub")]
        Team_B_Subs = 4,
    }
}
