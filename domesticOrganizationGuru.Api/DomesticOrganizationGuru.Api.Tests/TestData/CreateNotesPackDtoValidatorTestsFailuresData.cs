using domesticOrganizationGuru.Common.Dto;
using System.Collections;
using System.Collections.Generic;

namespace DomesticOrganizationGuru.Api.Tests.TestData
{
    public class CreateNotesPackDtoValidatorTestsFailuresData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new NoteInitialSettingsDto()
                    {
                        ExpirationMinutesRange = 1,
                        NoteName = string.Empty
                    },
                false,
                true
            };

            yield return new object[]
            {
                new NoteInitialSettingsDto()
                    {
                        ExpirationMinutesRange = 1,
                        NoteName = new string('a', 101)
                    },
                false,
                true
            };

            yield return new object[]
            {
                new NoteInitialSettingsDto()
                    {
                        ExpirationMinutesRange = 1,
                        NoteName = null
                    },
                false,
                true
            };

            yield return new object[]
            {
                new NoteInitialSettingsDto()
                    {
                        ExpirationMinutesRange = 0,
                        NoteName = "aaa"
                    },
                true,
                false
            };

            yield return new object[]
            {
                new NoteInitialSettingsDto()
                    {
                        ExpirationMinutesRange = 0,
                        NoteName = string.Concat("a", 101)
                    },
                false,
                false
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
