using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using CadFuncionario.AppService.cs;
using CadFuncionario.Domain.Entities;
using CadFuncionario.Domain.Interfaces.Data;
using Moq.AutoMock;
using Xunit;

namespace CadFuncionario.Application.Tests
{
    public class FuncionarioAppServiceTest
    {
        private readonly Faker _faker;

        public FuncionarioAppServiceTest()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Adicionar funcionário com dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_AdicionarFuncionario_Invalido()
        {
            // Arrange
            var funcionario = MontarFuncionario(true);
            var mocker = new AutoMocker();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            // Act
            var result = await funcionarioAppService.AdicionarAsync(funcionario);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Adicionar funcionário com dados validos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_AdicionarFuncionario_Sucesso()
        {
            // Arrange
            var funcionario = MontarFuncionario();
            var mocker = new AutoMocker();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            // Act
            var result = await funcionarioAppService.AdicionarAsync(funcionario);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Atualizar funcionário com dados invalidos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_AtualizarFuncionario_Invalido()
        {
            // Arrange
            var funcionario = MontarFuncionario(true);
            var mocker = new AutoMocker();
            var funcionariooAppService = mocker.CreateInstance<FuncionarioAppService>();

            // Act
            var result = await funcionariooAppService.AtualizarAsync(funcionario);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Atualizar funcionário com dados validos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_AtualizarFuncionario_Sucesso()
        {
            // Arrange
            var funcionario = MontarFuncionario();
            var mocker = new AutoMocker();
            var funcionariooAppService = mocker.CreateInstance<FuncionarioAppService>();

            // Act
            var result = await funcionariooAppService.AtualizarAsync(funcionario);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Obter funcionário passando dados válidos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_ObterFuncionario_Sucesso()
        {
            // Arrange
            var funcionarioId = _faker.Random.Guid();
            var funcionarioMock = MontarFuncionario();
            var mocker = new AutoMocker();
            var mockFuncionarioRepository = mocker.GetMock<IFuncionarioRepository>();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            mockFuncionarioRepository.Setup(p => p.ObterAsync(funcionarioId))
                .Returns(Task.FromResult(funcionarioMock));


            // Act
            var result = await funcionarioAppService.ObterAsync(funcionarioId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(funcionarioMock, result);
        }

        [Fact(DisplayName = "Obter funcionário passando dados inválidos")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_ObterFuncionario_Invalido()
        {
            // Arrange
            var mocker = new AutoMocker();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            // Act
            var result = await funcionarioAppService.ObterAsync(Guid.Empty);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Obter todos os funcionários")]
        [Trait("Grupo", "Services")]
        public async Task FuncionarioAppService_ObterTodos_Sucesso()
        {
            // Arrange
            var listaFuncionariosMockResult = new List<Funcionario>
            {
                MontarFuncionario(),
                MontarFuncionario(),
                MontarFuncionario()
            };

            var mocker = new AutoMocker();
            var mockFuncionarioRepository = mocker.GetMock<IFuncionarioRepository>();
            var funcionarioAppService = mocker.CreateInstance<FuncionarioAppService>();

            mockFuncionarioRepository.Setup(p => p.ObterTodosAsync())
                .Returns(Task.FromResult(listaFuncionariosMockResult as ICollection<Funcionario>));


            // Act
            var result = await funcionarioAppService.ObterTodosAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(listaFuncionariosMockResult.Count, result.Count);
            Assert.Equal(listaFuncionariosMockResult, result.ToList());
        }

        private Funcionario MontarFuncionario(bool invalido = false)
        {
            if (invalido)
                return new Funcionario(Guid.Empty, Guid.Empty, "", "", "", "", null);

            return new Funcionario(Guid.Empty, _faker.Random.Guid(), "12345678901",
             "1234567891", _faker.Company.CompanyName(),
             "1234567890", _faker.Date.Recent());
        }
    }
}