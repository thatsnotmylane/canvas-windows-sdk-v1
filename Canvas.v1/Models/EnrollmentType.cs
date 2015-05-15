using System;

namespace Canvas.v1.Models
{
    [Flags]
    public enum EnrollmentType
    {
        StudentEnrollment,
        TeacherEnrollment,
        TaEnrollment,
        DesignerEnrollment,
        ObserverEnrollment,
    }
}