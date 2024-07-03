using System.ComponentModel;

namespace LibraryManagementBackend.Objects
{
    /// <summary>
    /// The possible availibilities of the books
    /// </summary>
    public enum Availability
    {
        [Description("Availability Not Set")]
        AvailabilityNotSet = 0,
        [Description("Availabile")]
        Available = 1,
        [Description("Unavailable")]
        Unavailable = 2,
        [Description("Lost Or Damaged")]
        LostOrDamaged = 3,
        [Description("Removed")]
        Removed = 4,

    }
}
