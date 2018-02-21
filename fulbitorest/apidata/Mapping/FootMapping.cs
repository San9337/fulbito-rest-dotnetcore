using apidata.DataContracts;
using model.Enums;
using model.Utils;

namespace apidata.Mapping
{
    public static class FootMapping
    {
        public static FootData Map(this Foot foot)
        {
            return new FootData()
            {
                Description = foot.GetDescription(),
                Id = (int)foot,
            };
        }
    }
}
