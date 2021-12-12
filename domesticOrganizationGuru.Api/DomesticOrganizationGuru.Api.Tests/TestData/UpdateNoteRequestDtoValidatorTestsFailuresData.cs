using domesticOrganizationGuru.Common.Dto;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DomesticOrganizationGuru.Api.Tests.TestData
{
    public class UpdateNoteRequestDtoValidatorTestsFailuresData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new UpdateNoteRequestDto()
                {
                    ExpirationMinutesRange = 0,
                    NotesPack = new NoteDto[]
                    {
                       new NoteDto()
                        {
                            NoteText = new string('a', 101),
                            IsComplete = true
                        }
                    },
                    NoteName = null,
                    ConnectionId =  null
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
