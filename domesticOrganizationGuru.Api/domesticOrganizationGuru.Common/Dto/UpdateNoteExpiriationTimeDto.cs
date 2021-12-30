using domesticOrganizationGuru.Common.Dto.HubModels;

namespace domesticOrganizationGuru.Common.Dto
{
    public class UpdateNoteExpiriationTimeDto : HubDustributionBaseDto
    {
        public string NoteName { get; set; }
        public int ExpirationMinutesRange { get; set; }
    }
}
