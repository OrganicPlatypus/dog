using System;

namespace domesticOrganizationGuru.Entities
{
    public class NotesPack
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public Note[] Notes { get; set; }

        //TODO: Usunąć
        public int ExpirationMinutesRange { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
    }
}
