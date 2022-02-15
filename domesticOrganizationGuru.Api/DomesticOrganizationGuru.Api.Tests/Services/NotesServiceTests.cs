using AutoMapper;
using domesticOrganizationGuru.AutoMapper.MappingProfiles;
using domesticOrganizationGuru.Common.CustomExceptions;
using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Entities;
using domesticOrganizationGuru.Repository;
using domesticOrganizationGuru.SignalR.Services;
using DomesticOrganizationGuru.Api.Application.Services;
using DomesticOrganizationGuru.Api.Application.Services.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DomesticOrganizationGuru.Api.Tests.Services
{

    public class NotesServiceTests
    {
        private const string NoteName = "CreateNewNote";

        private readonly Mock<INotesRepository> _mockNotesRepository;
        private readonly Mock<INotesNotificationsService> _mockNotesNotificationsService;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<ILogger<NotesService>> _mocklogger;
        private readonly IMapper _mapper;

        public NotesServiceTests()
        {
            _mapper = CreateMapper();
            _mockNotesRepository = new();
            _mockNotesNotificationsService = new();
            _mockPasswordHasher = new();
            _mocklogger = new();
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

            _mockNotesRepository.Setup(x =>
                x.GetNote(It.IsAny<string>()))
                .ReturnsAsync(notesPack )
                .Verifiable();

            _mockNotesNotificationsService.Setup(_ =>
                _.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()));

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object, _mockPasswordHasher.Object, _mocklogger.Object);

            //Act
            var notesSessionDto = await notesService.GetNotes("anyName", "anyPassword");

            //Assert
            _mockNotesRepository.Verify(m => m.GetNote(It.IsAny<string>()), Times.Once);
            Assert.Equal(notesPack.ExpirationMinutesRange, notesSessionDto.ExpirationMinutesRange);
            Assert.Equal(notesPack.Notes.Length, notesSessionDto.Notes.Length);
            Assert.True(notesPack.Notes.Length + notesSessionDto.Notes.Length >= 2);
        }

        [Fact]
        public async Task GetNotes_NoNote_Test()
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

            _mockNotesRepository.Setup(x =>
                x.GetNote(It.IsAny<string>()))
                .ReturnsAsync(default(NotesPack))
                .Verifiable();

            _mockNotesNotificationsService.Setup(_ =>
                _.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()));

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object, _mockPasswordHasher.Object, _mocklogger.Object);
            Func<Task<NotesSessionDto>> getNote = () => notesService.GetNotes("anyName", "anyPassword");

            //Act

            //Assert
            await Assert.ThrowsAsync<NoteNotFoundException>(getNote);
            _mockNotesRepository.Verify(m => m.GetNote(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateNote_HappyPath_Test()
        {
            //Arrange
            CreateNoteDto createNoteDto = new CreateNoteDto()
            {
                NoteName = "CreateNewNote",
                ExpirationMinutesRange = 10
            };

            _mockNotesRepository
                .Setup(x => x.CreateNote(It.IsAny<NotesPack>()))
                .Verifiable();

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object, _mockPasswordHasher.Object, _mocklogger.Object);

            //Act
            DateTimeOffset noteExpirationDate = await notesService.CreateNote(createNoteDto);
            NotesPack notePack = _mapper.Map<NotesPack>(createNoteDto);

            //Assert
            Assert.NotNull(notePack);
            _mockNotesRepository.Verify(c => c.CreateNote(It.IsAny<NotesPack>()), Times.Once());
        }

        [Fact]
        public async Task CreateNote_NotCreated_Test()
        {
            CreateNoteDto createNoteDto = new CreateNoteDto()
            {
                NoteName = "CreateNewNote"
            };

            _mockNotesRepository
                .Setup(x => x.CreateNote(It.IsAny<NotesPack>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object, _mockPasswordHasher.Object, _mocklogger.Object);

            Func<Task<DateTimeOffset>> createNote = () => notesService.CreateNote(createNoteDto);

            //Act
            
            //Assert
            await Assert.ThrowsAsync<CreateNotesException>(createNote);

            _mockNotesRepository.Verify(c => c.CreateNote(It.IsAny<NotesPack>()), Times.Once());
        }

        [Fact]
        public async Task UpdateNote_HappyPath_Test()
        {
            //Arrange
            UpdateNoteRequestDto updateNoteRequest = CreateUpdateRequestDto();

            _mockNotesRepository
                .Setup(x => x.UpdateNote(It.IsAny<NotesPack>()))
                .ReturnsAsync(true)
                .Verifiable();

            string channelNameReturn = string.Empty;
            string groupNameReturn = string.Empty;

            _mockNotesNotificationsService
                .Setup(x => x.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()))
                .Callback<string, string, string, NoteDto[]>((channelName, groupName, _, __) =>
                {
                    channelNameReturn = channelName;
                    groupNameReturn = groupName;
                })
                .Returns(Task.CompletedTask)
                .Verifiable();

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object, _mockPasswordHasher.Object, _mocklogger.Object);

            //Act
            NotesPack notePack = _mapper.Map<NotesPack>(updateNoteRequest);
            await notesService.UpdateNote(updateNoteRequest);

            //Assert
            Assert.Equal("UpdateNotesState", channelNameReturn);
            Assert.Equal(updateNoteRequest.NoteName, groupNameReturn);
            _mockNotesRepository
                .Verify(c => c.UpdateNote(It.IsAny<NotesPack>()), Times.Once());
            _mockNotesNotificationsService
                .Verify(c => c.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()), Times.Once());
        }

        [Fact]
        public async Task UpdateNote_UnableToFindNotes_Test()
        {
            //Arrange
            UpdateNoteRequestDto updateNoteRequest = CreateUpdateRequestDto();

            _mockNotesRepository
                .Setup(x => x.UpdateNote(It.IsAny<NotesPack>()))
                .ReturnsAsync(false)
                .Verifiable();

            string groupNameReturn = string.Empty;

            _mockNotesNotificationsService
                .Setup(x => x.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()))
                .Callback<string, string, string, NoteDto[]>((_, groupName, __, ___) =>
                {
                    groupNameReturn = groupName;
                })
                .Returns(Task.CompletedTask)
                .Verifiable();

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object, _mockPasswordHasher.Object, _mocklogger.Object);

            Func<Task> seveNote = () => notesService.UpdateNote(updateNoteRequest);

            //Act

            //Assert
            await Assert.ThrowsAsync<UpdateNotesException>(seveNote);
            _mockNotesRepository
                .Verify(c => c.UpdateNote(It.IsAny<NotesPack>()), Times.Once());
            _mockNotesNotificationsService
                .Verify(c => c.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()), Times.Never());
        }

        [Fact]
        public async Task UpdateNote_Distribution_Test()
        {
            //Arrange
            UpdateNoteRequestDto updateNoteRequest = CreateUpdateRequestDto();

            _mockNotesRepository
                .Setup(x => x.UpdateNote(It.IsAny<NotesPack>()))
                .ReturnsAsync(false)
                .Verifiable();

            string groupNameReturn = string.Empty;

            _mockNotesNotificationsService
                .Setup(x => x.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()))
                .Callback<string, string, string, NoteDto[]>((_, groupName, __, ___) =>
                {
                    groupNameReturn = groupName;
                })
                .Returns(Task.CompletedTask)
                .Verifiable();

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object, _mockPasswordHasher.Object, _mocklogger.Object);

            Func<Task> seveNote = () => notesService.UpdateNote(updateNoteRequest);

            //Act

            //Assert
            await Assert.ThrowsAsync<UpdateNotesException>(seveNote);
            _mockNotesRepository
                .Verify(c => c.UpdateNote(It.IsAny<NotesPack>()), Times.Once());
            _mockNotesNotificationsService
                .Verify(c => c.UpdateGroupNotesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NoteDto[]>()), Times.Never());
        }


        [Fact]
        public async Task UpdateNoteExpiriationTime_HappyPath_Test()
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

            _mockNotesRepository.Setup(x =>
                x.GetNote(It.IsAny<string>()))
                .ReturnsAsync(notesPack)
                .Verifiable();

            UpdateNoteExpiriationTimeDto updateNoteRequest = new()
            {
                ExpirationMinutesRange = 2,
                NoteName = "Valid note name"
            };

            _mockNotesRepository
                .Setup(x => x.UpdateNote(It.IsAny<NotesPack>()))
                .ReturnsAsync(true)
                .Verifiable();

            string channelNameReturn = string.Empty;
            string groupNameReturn = string.Empty;

            _mockNotesNotificationsService
                .Setup(x => x.UpdateGroupExpiriationTimeAsync(It.IsAny<string>(), It.IsAny<string>(),It.IsAny<DateTime>()))
                .Callback<string, string, DateTime>((channelName, groupName, _) =>
                {
                    channelNameReturn = channelName;
                    groupNameReturn = groupName;
                })
                .Returns(Task.CompletedTask)
                .Verifiable();

            NotesService notesService = new(_mockNotesRepository.Object, _mapper, _mockNotesNotificationsService.Object, _mockPasswordHasher.Object, _mocklogger.Object);

            //Act
            NotesPack notePack = _mapper.Map<NotesPack>(updateNoteRequest);
            await notesService.UpdateNoteExpiriationTimeAsync(updateNoteRequest);

            //Assert
            Assert.Equal("UpdateExpiriationTimeState", channelNameReturn);
            Assert.Equal(updateNoteRequest.NoteName, groupNameReturn);
            _mockNotesRepository
                .Verify(c => c.UpdateNote(It.IsAny<NotesPack>()), Times.Once());
            _mockNotesNotificationsService
                .Verify(c => c.UpdateGroupExpiriationTimeAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once());
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

        private static UpdateNoteRequestDto CreateUpdateRequestDto() => new()
            {
                NoteName = NoteName,
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
    }
}