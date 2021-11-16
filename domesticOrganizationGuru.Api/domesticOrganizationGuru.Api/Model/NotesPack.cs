using System.Collections.Generic;

namespace DomesticOrganizationGuru.Api.Model
{
    public class NotesPack
    {
        public string Password { get; set; }
        public Note[] Notes{ get; set; }
        public int ExpirationMinutesRange { get; set; }
    }
}
