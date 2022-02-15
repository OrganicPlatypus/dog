using System;

namespace domesticOrganizationGuru.Common.CustomExceptions
{
    [Serializable]
    public class NoteNotFoundException : Exception
    {
        public string NoteName { get; }

        public NoteNotFoundException() { }

        public NoteNotFoundException(string key)
            : base($"Note was not found") { }
    }
}
