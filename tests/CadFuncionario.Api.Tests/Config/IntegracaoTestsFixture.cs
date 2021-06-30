using System.Net.Http;
using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Bogus;
using CadFuncionario.Domain.Entities;

namespace CadFuncionario.Api.Tests.Config
{
    [CollectionDefinition(nameof(IntegracaoTestsFixtureCollection))]
    public class IntegracaoTestsFixtureCollection : ICollectionFixture<IntegracaoTestsFixture> { }

    public class IntegracaoTestsFixture : IDisposable
    {
        private readonly ApiFactory<Startup> _factory;

        public HttpClient Client;
        public Faker Faker;
        public Guid ProfissaoId;
        public Guid StepProfissaoId;
        public Profissao ProfissaoApi;
        public StepProfissao StepProfissaoApi;
        public Funcionario FuncionarioApi;
        public Guid FuncionarioId;

        public IntegracaoTestsFixture()
        {
            Faker = new Faker("pt_BR");

            _factory = new ApiFactory<Startup>();
            Client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            { BaseAddress = new Uri("http://localhost:5000") });
        }


        public void Dispose()
        {
            Client.Dispose();
            _factory.Dispose();
        }
    }
}
