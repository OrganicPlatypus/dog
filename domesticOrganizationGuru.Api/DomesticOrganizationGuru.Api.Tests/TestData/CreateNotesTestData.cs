using domesticOrganizationGuru.Common.Dto;
using System.Collections;
using System.Collections.Generic;

namespace DomesticOrganizationGuru.Api.Tests.TestData
{
    public class CreateNotesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //yield return new object[]
            //{
            //    new CreateNotesPackDto()
            //    {
            //        NoteName = "NoteName",
            //        ExpirationMinutesRange = 0
            //    },
            //    false
            //};

            //yield return new object[]
            //{
            //    new CreateNotesPackDto()
            //    {
            //        NoteName = "",
            //        ExpirationMinutesRange = 0
            //    },
            //    false
            //};

            //yield return new object[]
            //{
            //    new CreateNotesPackDto()
            //    {
            //        NoteName = null,
            //        ExpirationMinutesRange = 0
            //    },
            //    false
            //};

            //yield return new object[]
            //{
            //    new CreateNotesPackDto()
            //    {
            //        NoteName = "",
            //        ExpirationMinutesRange = 1
            //    },
            //    false
            //};

            yield return new object[]
            {
                new CreateNotesPackDto()
                {
                    NoteName = "CreateNewNote",
                    ExpirationMinutesRange = 1
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
