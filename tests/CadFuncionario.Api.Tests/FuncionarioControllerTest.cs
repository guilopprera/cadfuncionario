using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CadFuncionario.Api.Tests.Config;
using CadFuncionario.Domain.Entities;
using Xunit;

namespace CadFuncionario.Api.Tests
{
    [TestCaseOrderer("CadFuncionario.Api.Tests.Config.PriorityOrderer", "CadFuncionario.Api.Tests")]
    [Collection(nameof(IntegracaoTestsFixtureCollection))]
    public class FuncionarioControllerTest
    {
        private readonly IntegracaoTestsFixture _testsFixture;

        public FuncionarioControllerTest(IntegracaoTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar funcionário"), TestPriority(1)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task FuncionarioController_Adicionar()
        {
            // Arrange
            var faker = _testsFixture.Faker;

            var funcionario = new Funcionario(Guid.Empty, faker.Random.Guid(), "12345678901", "1234567891", faker.Company.CompanyName(), "1234567890", faker.Date.Recent());

            _testsFixture.FuncionarioId = funcionario.FuncionarioId;

            // Act
            var response = await _testsFixture.Client
                .PostAsJsonAsync("api/funcionario/adicionar", funcionario);

            // Assert
            response.EnsureSuccessStatusCode();

            var retornoApi = await response.Content.ReadAsAsync<bool>();
            Assert.True(retornoApi);
        }

        [Fact(DisplayName = "Obter funcionário"), TestPriority(2)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task FuncionarioController_Obter()
        {
            // Arrange & Act
            var response = await _testsFixture.Client
                .GetAsync($"api/funcionario/obter/{_testsFixture.FuncionarioApi}");

            // Assert
            response.EnsureSuccessStatusCode();

            _testsFixture.FuncionarioApi = await response.Content.ReadAsAsync<Funcionario>();

            Assert.NotNull(_testsFixture.FuncionarioApi);
            Assert.NotEqual(Guid.Empty, _testsFixture.FuncionarioApi.FuncionarioId);
        }

        [Fact(DisplayName = "Atualizar funcionário"), TestPriority(3)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task FuncionarioController_Atualizar()
        {
            // Arrange & Act
            var response = await _testsFixture.Client
                .GetAsync($"api/funcionario/atualizar/{_testsFixture.FuncionarioApi}");

            // Assert
            response.EnsureSuccessStatusCode();

            _testsFixture.FuncionarioApi = await response.Content.ReadAsAsync<Funcionario>();

            Assert.NotNull(_testsFixture.FuncionarioApi);
            Assert.NotEqual(Guid.Empty, _testsFixture.FuncionarioApi.FuncionarioId);
        }

        [Fact(DisplayName = "Atualizar funcionário"), TestPriority(4)]
        [Trait("Grupo", "IntegracaoAPI")]
        public async Task FuncionarioController_ObterTodosFuncionarios()
        {
            // Arrange & Act
            var response = await _testsFixture.Client.GetAsync("api/funcionario/obtertodos");

            // Assert
            response.EnsureSuccessStatusCode();

            var listaFuncionario = await response.Content.ReadAsAsync<List<Funcionario>>();
            Assert.NotEmpty(listaFuncionario);
        }
    }
}