namespace domesticOrganizationGuru.Entities
{
    public class NotesPack
    {
        public string Password { get; set; }
        public Note[] Notes { get; set; }
        public int ExpirationMinutesRange { get; set; }
    }
}
