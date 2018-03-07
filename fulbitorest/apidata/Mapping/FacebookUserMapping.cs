using apidata.DataContracts.External;
using model.Business.Structures;

namespace apidata.Mapping
{
    public static class FacebookUserMapping
    {
        public static FacebookUser Map(this FacebookUserViewModel fbViewModel, string fbToken)
        {
            var fbUser = fbViewModel.MapTo<FacebookUser>();
            fbUser.IssuedToken = fbToken;
            return fbUser;
        }
    }
}
