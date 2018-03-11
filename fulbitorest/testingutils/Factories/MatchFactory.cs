using Microsoft.CodeAnalysis.Differencing;
using System;
using System.Collections.Generic;
using System.Text;


namespace testingutils.Factories
{
    public static class MatchFactory
    {
        public static model.Model.Match Get()
        {
            return new model.Model.Match(UserFactory.Get())
            {
                Id = 1,
                GameAddress = "address",
                StartDateTime = DateTime.Now,
                DurationInMinutes = 60,
                GameFieldSize = 5,
                MainPlayersTeamSize = 3,
                SubstitutePlayersTeamSize = 1,
                RequiresApproval = false
            };
        }
    }
}
