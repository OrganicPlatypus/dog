using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Common.Resources.Validation;
using System.Collections;
using System.Collections.Generic;

namespace DomesticOrganizationGuru.Api.Tests.TestData
{
    public class NoteDtoValidatorTestsFailuresData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new NoteDto()
                    {
                        NoteText = null,
                        IsComplete = false,
                    },
                ValidationMessages.NoteEmptyInformation
            };

            yield return new object[]
            {
                new NoteDto()
                    {
                        NoteText = new string('a', 101),
                        IsComplete = true
                    },
                string.Format($"{ValidationMessages.NoteMaxLengthPrefix}{ValidationSettings.MaxNoteLength}{ValidationMessages.NoteMaxLengthSufix}")
            };

            yield return new object[]
            {
                new NoteDto()
                    {
                        NoteText = string.Empty,
                        IsComplete = true
                    },
                ValidationMessages.NoteEmptyInformation
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
