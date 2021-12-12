using domesticOrganizationGuru.Common.Dto;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DomesticOrganizationGuru.Api.Tests.TestData
{
    public class NotesSessionDtoValidatorTestsSuccessData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new NotesSessionDto()
                {
                    ExpirationMinutesRange = 1,
                    Notes = Array.Empty<NoteDto>()
                }
            };

            yield return new object[]
{
                new NotesSessionDto()
                {
                    ExpirationMinutesRange = 1,
                    Notes =  new NoteDto[]
                    {
                        new NoteDto
                        {
                            NoteText = "Good one",
                            IsComplete = false
                        }
                    }
                }
};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
