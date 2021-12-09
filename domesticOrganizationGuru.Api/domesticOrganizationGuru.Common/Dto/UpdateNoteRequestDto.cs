using domesticOrganizationGuru.Common.Dto.HubModels;

namespace domesticOrganizationGuru.Common.Dto
{
    public class UpdateNoteRequestDto : HubDustributionBaseDto
    {
        public string NoteName { get; set; }
        public NoteDto[] NotesPack { get; set; }
        public int ExpirationMinutesRange { get; set; }
    }
}
