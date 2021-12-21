using System;

namespace domesticOrganizationGuru.Common.CustomExceptions
{
    [Serializable]
    public class NotAbleToDistributeToGroupException : Exception
    {
        public string NoteName { get; }

        public NotAbleToDistributeToGroupException() { }

        public NotAbleToDistributeToGroupException(string key)
            : base($"Not distributed to {key} viewers") { }
    }
}
