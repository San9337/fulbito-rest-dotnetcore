using apidata.DataContracts;
using model.Enums;
using model.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace apidata.Mapping
{
    public static class GenderMapping
    {
        public static GenderData Map(this Gender gender)
        {
            return new GenderData()
            {
                Description = gender.GetDescription(),
                Id = (int)gender,
            };
        }
    }
}
