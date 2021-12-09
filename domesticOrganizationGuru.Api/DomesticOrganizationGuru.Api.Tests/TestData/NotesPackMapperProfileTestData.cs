using domesticOrganizationGuru.Common.Dto;
using System.Collections;
using System.Collections.Generic;

namespace DomesticOrganizationGuru.Api.Tests.TestData
{
    public class NotesPackMapperProfileTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new UpdateNoteRequestDto
                {
                    ConnectionId = "ConnectionId",
                    NoteName = "From UpdateNoteRequestDto",
                    NotesPack = new NoteDto[]
                    {
                        new NoteDto
                        {
                            IsComplete = true,
                            NoteText = "New text"
                        }
                    },
                    ExpirationMinutesRange = 55
                }

            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
