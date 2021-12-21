using System;

namespace domesticOrganizationGuru.Common.CustomExceptions
{
    [Serializable]
    public class UpdateNotesException : Exception
    {
        public string NoteName { get; }

        public UpdateNotesException()
            : base($"Could not update note") { }
    }
}
