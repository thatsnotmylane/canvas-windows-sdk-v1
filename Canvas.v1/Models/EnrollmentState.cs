using System;

namespace Canvas.v1.Models
{
    [Flags]
    public enum EnrollmentState
    {
        Active,
        Invited,
        Creation_Pending,
        Deleted,
        Rejected,
        Completed,
        Inactive,
    }
}