namespace DomesticOrganizationGuru.Api.Model.Dto
{
    public class UpdateNoteRequestDto
    {
        public string NoteName { get; set; }
        public NoteDto[] NotesPack { get; set; }
        public int ExpirationMinutesRange { get; set; }
    }
}
