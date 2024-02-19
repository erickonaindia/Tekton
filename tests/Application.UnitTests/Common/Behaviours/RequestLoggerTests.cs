using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Tekton.Application.Common.Behaviours;
using Tekton.Application.Common.Interfaces;
using Tekton.Application.Products.Commands.CreateProduct;

namespace Tekton.Application.UnitTests.Common.Behaviours;
public class RequestLoggerTests
{
    private Mock<ILogger<CreateProductCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateProductCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateProductCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateProductCommand {
            Description = "TestDescription",
            Name = "TestName",
            Price = 1,
            StatusName = "Activate",
            Stock = 1
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateProductCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateProductCommand {
            Description = "TestDescription",
            Name = "TestName",
            Price = 1,
            StatusName = "Activate",
            Stock = 1
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
