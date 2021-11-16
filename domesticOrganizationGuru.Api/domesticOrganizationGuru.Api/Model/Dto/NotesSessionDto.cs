namespace DomesticOrganizationGuru.Api.Model.Dto
{
    public class NotesSessionDto
    {
        public NoteDto[] Notes { get; set; }
        public int ExpirationMinutesRange { get; set; }
    }
}