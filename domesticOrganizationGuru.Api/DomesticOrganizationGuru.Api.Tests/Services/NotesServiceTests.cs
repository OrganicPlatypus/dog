using AutoMapper;
using domesticOrganizationGuru.AutoMapper.MappingProfiles;
using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Entities;
using domesticOrganizationGuru.Repository;
using DomesticOrganizationGuru.Api.Application.Services;
using DomesticOrganizationGuru.Api.Application.Services.Implementation;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DomesticOrganizationGuru.Api.Tests.Services
{

    public class NotesServiceTests
    {
        private readonly Mock<INotesRepository> _mockNotesRepository;
        private readonly Mock<INotesNotificationsService> _mockNotesNotificationsService;
        private readonly IMapper _mapper;

        public NotesServiceTests()
        {
            _mapper = CreateMapper();
            _mockNotesRepository = new();
            _mockNotesNotificationsService = new();
        }

        [Fact]
        public async Task GetNotes_HappyPath_Test()
        {
            //Arrange
            NotesPack notesPack = new()
            {
                ExpirationMinutesRange = 59,
                Notes = new[] { new Note()
                    {
                        IsComplete = true,
                        NoteText = string.Empty,
                    }
                },
                Password = "new name"
            };
            NoteDto[] notesDto = new[] {
                new NoteDto
                    {
                        IsComplete=true,
                        NoteText="note",
                    }
            };

            _mockNotesRepository.Setup(x =>
                x.GetNote(It.IsAny<string>()))
                .ReturnsAsync(notesPack)
                .Verifiable();

            _mockNotesNotificationsService.Setup(_ =>
                _.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()));

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object);

            //Act
            var notesSessionDto = await notesService.GetNotes("anyName");

            //Assert
            _mockNotesRepository.Verify(m => m.GetNote(It.IsAny<string>()), Times.Once);
            Assert.Equal(notesPack.ExpirationMinutesRange, notesSessionDto.ExpirationMinutesRange);
            Assert.Equal(notesPack.Notes.Length, notesSessionDto.Notes.Length);
            Assert.True(notesPack.Notes.Length + notesSessionDto.Notes.Length >= 2);
        }

        [Fact]
        public async Task CreateNote_HappyPath_Test()
        {
            CreateNotesPackDto createNotesPackDto = new CreateNotesPackDto()
            {
                NoteName = "CreateNewNote",
                ExpirationMinutesRange = 1
            };

            _mockNotesRepository
                .Setup(x => x.CreateNote(It.IsAny<NotesPack>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object);

            //Act
            string createdNoteName = await notesService.CreateNote(createNotesPackDto);
            NotesPack notePack = _mapper.Map<NotesPack>(createNotesPackDto);

            //Assert
            Assert.Equal(createNotesPackDto.NoteName, createdNoteName);

            _mockNotesRepository.Verify(c => c.CreateNote(It.IsAny<NotesPack>()), Times.Once());
        }

        [Fact]
        public async Task UpdateNote_HappyPath_Test()
        {
            //Arrange
            const string noteName = "CreateNewNote";

            CreateNotesPackDto createNotesPackDto = new()
            {
                NoteName = noteName,
                ExpirationMinutesRange = 1
            };

            UpdateNoteRequestDto updateNoteRequest = new()
            {
                NoteName = noteName,
                NotesPack = new[]
                {
                    new NoteDto
                    {
                        IsComplete = true,
                        NoteText = "Updated note"
                    }
                },
                ExpirationMinutesRange = 999,
                ConnectionId = "someConnectionId"

            };

            _mockNotesRepository
                .Setup(x => x.UpdateNote(It.IsAny<NotesPack>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            string groupNameReturn = "";

            _mockNotesNotificationsService
                .Setup(x => x.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()))
                .Callback<string, string, string, NoteDto[]>((_, groupName, __, ___) =>
                {
                    groupNameReturn = groupName;
                })
                .Returns(Task.CompletedTask).Verifiable();

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object);

            //Act
            NotesPack notePack = _mapper.Map<NotesPack>(updateNoteRequest);
            await notesService.SaveNote(updateNoteRequest);

            //Assert
            Assert.Equal(updateNoteRequest.NoteName, groupNameReturn);
            _mockNotesRepository
                .Verify(c => c.UpdateNote(It.IsAny<NotesPack>()), Times.Once());
            _mockNotesNotificationsService
                .Verify(c => c.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()), Times.Once());
        }

        private static IMapper CreateMapper()
        {
            MapperConfiguration mapperConfiguration = new(configuration =>
            {
                configuration.AddProfile<NotesPackMapperProfile>();
                configuration.AddProfile<NoteMapperProfile>();
            });
            var mapper = mapperConfiguration.CreateMapper();
            return mapper;
        }
    }
}