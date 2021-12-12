using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Common.Resources.Validation;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DomesticOrganizationGuru.Api.Tests.TestData
{
    public class NotesSessionDtoValidatorTestsFailuresData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new NotesSessionDto()
                {
                    ExpirationMinutesRange = 0,
                    Notes = Array.Empty<NoteDto>()
                },
                true,
                false,
                ValidationMessages.LifetimeInformation
            };

            yield return new object[]
            {
                new NotesSessionDto()
                {
                    ExpirationMinutesRange = 1,
                    Notes = new NoteDto[]
                    {
                        new NoteDto()
                            {
                                NoteText = null,
                                IsComplete = false,
                            },
                    }
                },
                false,
                true,
                ValidationMessages.NoteEmptyInformation
            };

            yield return new object[]
            {
                new NotesSessionDto()
                {
                    ExpirationMinutesRange = 1,
                    Notes = new NoteDto[]
                    {
                        new NoteDto()
                            {
                                NoteText = new string('a', 101),
                                IsComplete = true
                            },
                    }
                },
                false,
                true,
                string.Format($"{ValidationMessages.NoteMaxLengthPrefix}{ValidationSettings.MaxNoteLength}{ValidationMessages.NoteMaxLengthSufix}")
            };

            yield return new object[]
            {
                new NotesSessionDto()
                {
                    ExpirationMinutesRange = 1,
                    Notes = new NoteDto[]
                    {
                        new NoteDto()
                            {
                                NoteText = string.Empty,
                                IsComplete = true
                            },
                    }
                },
                false,
                true,
                ValidationMessages.NoteEmptyInformation
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}