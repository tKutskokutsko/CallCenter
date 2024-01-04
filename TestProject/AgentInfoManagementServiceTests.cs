using Bogus;
using CallCenterApi.Infrastructure.DB.Entities;
using CallCenterApi.Infrastructure.DB.Repositories;
using CallCenterApi.Models;
using CallCenterApi.Services;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace TestProject;

[TestFixture]
public class AgentInfoManagementServiceTests
{
    private readonly Faker _faker = new();

    private Fake<IAgentInfoRepository>? _agentInfoRepositoryFake;
    private Fake<IUnitOfWork>? _unitOfWorkFake;

    [SetUp]
    public void Setup()
    {
        _agentInfoRepositoryFake = new Fake<IAgentInfoRepository>();
        _unitOfWorkFake = new Fake<IUnitOfWork>();
    }

    [Test]
    public async Task CheckAgentState_AgentInfoNotPresent_ShouldCreateAgentInfo()
    {
        // Arrange
        var sut = BuildSut();

        var agentInfoModel = new AgentInfoModel()
        {
            AgentId = Guid.NewGuid(),
            TimestampUtc = DateTime.Now,
            Action = "CALL_STARTED",
            AgentName = _faker.Person.FullName,
        };

        // Act
        await sut.CheckAgentState(agentInfoModel);

        // Assert
        using var scope = new AssertionScope();

        _agentInfoRepositoryFake?.CallsTo(repository => repository.Get(A<Guid>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task CheckAgentState_AgentInfoIsPresent_ShouldReturnAgentStateResponseModel()
    {
        // Arrange
        var sut = BuildSut();

        var agentId = Guid.NewGuid();
        var agentName = _faker.Person.FullName;
        var agentInfoModel = new AgentInfoModel()
        {
            AgentId = agentId,
            TimestampUtc = DateTime.Now,
            Action = "CALL_STARTED",
            AgentName = agentName,
        };

        var agentInfo = AgentInfo.Create(agentId, agentName, DateTime.Now, "CALL_STARTED", null, agentName);
        _agentInfoRepositoryFake?.CallsTo(repository => repository.Get(A<Guid>._)).Returns(agentInfo);

        // Act
        await sut.CheckAgentState(agentInfoModel);

        // Assert
        using var scope = new AssertionScope();

        _agentInfoRepositoryFake?.CallsTo(repository => repository.Get(A<Guid>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task CheckAgentState_AgentInfoIsPresentEventIsLate_ShouldReturnAgentStateResponseModel()
    {
        // Arrange
        var agentId = Guid.NewGuid();
        var agentName = _faker.Person.FullName;
        var agentInfoModel = new AgentInfoModel()
        {
            AgentId = agentId,
            TimestampUtc = DateTime.Now.AddHours(-3),
            AgentName = agentName,
        };

        var agentInfo = AgentInfo.Create(agentId, agentName, DateTime.Now, "CALL_STARTED", null, agentName);
        _agentInfoRepositoryFake?.CallsTo(repository => repository.Get(A<Guid>._)).Returns(agentInfo);


        // Act
        var sut = BuildSut();

        Func<Task<AgentStateResponseModel?>> func = async () => await sut.CheckAgentState(agentInfoModel);

        // Assert
        await func.Should().ThrowAsync<LateEventException>();
    }

    private AgentInfoManagementService BuildSut() =>
        new(_agentInfoRepositoryFake!.FakedObject,
            _unitOfWorkFake!.FakedObject);
}