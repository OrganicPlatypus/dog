using AutoMapper;
using domesticOrganizationGuru.AutoMapper.MappingProfiles;
using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Entities;
using DomesticOrganizationGuru.Api.Tests.TestData;
using Xunit;

namespace DomesticOrganizationGuru.Api.Tests.Mappings
{
    public class MapperConfigurationsTests
    {
        [Fact]
        public void NoteMapperProfile_Configuration_IsValid()
        {
            //Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<NoteMapperProfile>());

            //Act
            //Assert
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void NotesPackMapperProfile_Configuration_IsValid()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<NotesPackMapperProfile>();
                cfg.AddProfile<NoteMapperProfile>();
            });

            //Act
            //Assert
            config.AssertConfigurationIsValid();
        }

        [Theory]
        [ClassData(typeof(NotesPackMapperProfileTestData))]
        public void NotesPackMapperProfile_UpdateNoteRequestDto_Example_Test(UpdateNoteRequestDto updateNoteRequestDto)
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<NotesPackMapperProfile>();
                cfg.AddProfile<NoteMapperProfile>();
            });
            var mapper = config.CreateMapper();

            //Act
            var mappedInto = mapper.Map<NotesPack>(updateNoteRequestDto);

            //Assert
            Assert.IsType<NotesPack>(mappedInto);
            Assert.Equal(mappedInto.ExpirationMinutesRange, updateNoteRequestDto.ExpirationMinutesRange);
            Assert.Equal(mappedInto.Password, updateNoteRequestDto.NoteName);
            Assert.True(mappedInto.Notes.Length > 0 && updateNoteRequestDto.NotesPack.Length > 0);
            Assert.Equal(mappedInto.Notes.Length, updateNoteRequestDto.NotesPack.Length);
            Assert.Equal(mappedInto.Notes[0].NoteText, updateNoteRequestDto.NotesPack[0].NoteText);
            Assert.Equal(mappedInto.Notes[0].IsComplete, updateNoteRequestDto.NotesPack[0].IsComplete);
        }


        [Fact]
        public void NotesPackMapperProfile_UpdateNoteExpiriationTimeDto_Example_Test()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<NotesPackMapperProfile>();
                cfg.AddProfile<NoteMapperProfile>();
            });

            UpdateNoteExpiriationTimeDto updateNoteExpiriationTimeDto = new()
            {
                ExpirationMinutesRange = 2,
                NoteName = "Valid note name"
            };
            var mapper = config.CreateMapper();

            //Act
            var mappedInto = mapper.Map<NotesPack>(updateNoteExpiriationTimeDto);

            //Assert
            Assert.IsType<NotesPack>(mappedInto);
            Assert.Equal(mappedInto.ExpirationMinutesRange, updateNoteExpiriationTimeDto.ExpirationMinutesRange);
            Assert.Equal(mappedInto.Password, updateNoteExpiriationTimeDto.NoteName);
            Assert.Null(mappedInto.Notes);
        }

        [Fact]
        public void NotesPackMapperProfile_NotesPack_Example_Test()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<NotesPackMapperProfile>();
                cfg.AddProfile<NoteMapperProfile>();
            });
            var mapper = config.CreateMapper();

            var notesSessionDto = new NotesPack
            {
                Password = "To be ignored",
                Notes = new Note[]
                    {
                        new Note
                        {
                            IsComplete = true,
                            NoteText = "New text"
                        }
                    },
                ExpirationMinutesRange = 64
            };

            //Act
            var mappedInto = mapper.Map<NotesSessionDto>(notesSessionDto);

            //Assert
            Assert.IsType<NotesSessionDto>(mappedInto);
            Assert.Equal(mappedInto.ExpirationMinutesRange, notesSessionDto.ExpirationMinutesRange);
            Assert.True(mappedInto.Notes.Length > 0 && notesSessionDto.Notes.Length > 0);
            Assert.Equal(mappedInto.Notes.Length, notesSessionDto.Notes.Length);
            Assert.Equal(mappedInto.Notes[0].NoteText, notesSessionDto.Notes[0].NoteText);
            Assert.Equal(mappedInto.Notes[0].IsComplete, notesSessionDto.Notes[0].IsComplete);
        }
    }
}
