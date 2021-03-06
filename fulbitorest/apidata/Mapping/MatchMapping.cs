﻿using apidata.DataContracts;
using apidata.Utils;
using model.Business;
using model.Model;

namespace apidata.Mapping
{
    public static class MatchMapping
    {
        public static MatchData Map(this Match match)
        {
            var data = match.MapTo<MatchData>();

            data.StartDateTime = DataStandards.FormatDateTime(match.StartDateTime);
            data.Location = match.Location.Map();

            return data;
        }

        public static MatchSummaryData MapSummary(this MatchSummary summary)
        {
            var data = summary.MapTo<MatchSummaryData>();

            data.StartDateTime = DataStandards.FormatDateTime(summary.StartDateTime);

            return data;
        }
    }
}
