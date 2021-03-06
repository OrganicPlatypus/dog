using System;

namespace domesticOrganizationGuru.Common.Dto
{
    public class NotesSessionDto
    {
        public NoteDto[] Notes { get; set; }
        public int ExpirationMinutesRange { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
    }
}