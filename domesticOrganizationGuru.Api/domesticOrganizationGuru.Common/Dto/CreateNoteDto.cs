﻿namespace domesticOrganizationGuru.Common.Dto
{
    public class CreateNoteDto
    {
        public string NoteName { get; set; }
        public string Password { get; set; }
        public int ExpirationMinutesRange { get; set; }
    }
}
