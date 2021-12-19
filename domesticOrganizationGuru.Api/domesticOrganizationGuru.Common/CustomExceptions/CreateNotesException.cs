using System;

namespace domesticOrganizationGuru.Common.CustomExceptions
{
    [Serializable]
    public class CreateNotesException : Exception
    {
        public string NoteName { get; }

        public CreateNotesException() { }

        public CreateNotesException(string key)
            : base($"Name {key} already exists") { }
    }
}
