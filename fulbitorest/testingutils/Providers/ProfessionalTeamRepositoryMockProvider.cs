using System;
using System.Collections.Generic;
using System.Text;
using datalayer.Contracts.Repositories;
using Moq;
using model.Model;

namespace testingutils.Providers
{
    public static class ProfessionalTeamRepositoryMockProvider
    {
        public static Mock<IProfessionalTeamRepository> Get()
        {
            var mock = new Mock<IProfessionalTeamRepository>();

            mock.Setup(m => m.GetDefaultValue()).Returns(ProfessionalTeam.UNDEFINED);

            return mock;
        }
    }
}
