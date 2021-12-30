using domesticOrganizationGuru.Common.Dto;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DomesticOrganizationGuru.Api.Tests.TestData
{
    public class UpdateNoteExpiriationTimeDtoTestsFailuresData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new UpdateNoteExpiriationTimeDto()
                {
                    ExpirationMinutesRange = 0,
                    NoteName = null,
                    ConnectionId =  null
                }
            };

            yield return new object[]
            {
                new UpdateNoteExpiriationTimeDto()
                {
                    ExpirationMinutesRange = 0,
                    NoteName = string.Empty,
                    ConnectionId =  string.Empty
                }
            };
            yield return new object[]
            {
                new UpdateNoteExpiriationTimeDto()
                {
                    ExpirationMinutesRange = 0,
                    NoteName = new string('a', 101),
                    ConnectionId =  null
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
