namespace TruckDemo_v1.Domain.Entities
{
    public class UserOculusCode
    {
        public UserOculusCode(Guid userId, string oculusCode)
        {
            UserId = userId;
            OculusCode = oculusCode;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OculusCode { get; set; }


    }
}
