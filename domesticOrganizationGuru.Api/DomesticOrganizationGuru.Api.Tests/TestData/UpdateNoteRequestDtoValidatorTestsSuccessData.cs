using domesticOrganizationGuru.Common.Dto;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DomesticOrganizationGuru.Api.Tests.TestData
{
    public class UpdateNoteRequestDtoValidatorTestsSuccessData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new UpdateNoteRequestDto()
                {
                    ExpirationMinutesRange = 1,
                    NotesPack = Array.Empty<NoteDto>(),
                    NoteName = "Correct Name",
                    ConnectionId =  "Correct connection Id"
                }
            };

            yield return new object[]
{
                new UpdateNoteRequestDto()
                {
                    ExpirationMinutesRange = 1,
                    NotesPack = new NoteDto[]
                    {
                        new NoteDto
                        {
                            NoteText = "Good one",
                            IsComplete = false
                        }
                    },
                    NoteName = "Correct Name",
                    ConnectionId =  "Correct connection Id"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
