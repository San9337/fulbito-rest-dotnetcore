using apidata.DataContracts;
using apidata.Utils;
using model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace apidata.Mapping
{
    public static class MatchMapping
    {
        public static MatchData Map(this Match match)
        {
            var data = match.MapTo<MatchData>();

            data.StartDateTime = DataStandards.FormatDateTime(match.StartDateTime);

            return data;
        }
    }
}
