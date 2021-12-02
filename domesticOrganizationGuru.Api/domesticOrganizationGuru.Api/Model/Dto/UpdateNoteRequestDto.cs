using DomesticOrganizationGuru.Api.Model.HubModels;

namespace DomesticOrganizationGuru.Api.Model.Dto
{
    public class UpdateNoteRequestDto: HubDustributionBaseDto
    {
        public string NoteName { get; set; }
        public NoteDto[] NotesPack { get; set; }
        public int ExpirationMinutesRange { get; set; }
    }
}
