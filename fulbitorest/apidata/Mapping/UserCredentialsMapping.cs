using apidata.DataContracts;
using model.Business.Structures;

namespace apidata.Mapping
{
    public static class UserCredentialsMapping
    {
        public static FulbitoUser MapToFulbitoUser(this UserCredentialsData data)
        {
            return new FulbitoUser()
            {
                NickName = data.NickName, 
                Email = data.Email,
                Password = data.Password
            };
        }
    }
}
