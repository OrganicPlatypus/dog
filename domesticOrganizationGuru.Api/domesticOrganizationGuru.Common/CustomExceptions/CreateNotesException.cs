using System;

namespace domesticOrganizationGuru.Common.CustomExceptions
{
    [Serializable]
    public class CreateNotesException : Exception
    {
        public string NoteName { get; }

        public CreateNotesException()
            : base($"Note name is not acceptable") { }
    }
}
