using System;

namespace domesticOrganizationGuru.Entities
{
    public class NotesPack
    {
        //TODO: remove password from here WTF
        public string Password { get; set; }
        public Note[] Notes { get; set; }
        public int ExpirationMinutesRange { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
    }
}
