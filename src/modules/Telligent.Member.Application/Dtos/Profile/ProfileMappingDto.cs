namespace Telligent.Member.Application.Dtos.Profile
{
    public class ProfileMappingDto
    {
        public IList<ProfileDto> Profiles { get; set; }

        public IList<CustomProfileDto> CustomProfiles { get; set; }
    }
}
