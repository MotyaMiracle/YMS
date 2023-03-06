using Domain.Shared;

namespace Domain.Extentions
{
    public static class LookUpExtentions
    {
        public static Guid? TryGetValue(this LookUpDto lookUpDto)
        {
            var isGuid = Guid.TryParse(lookUpDto.Value, out Guid result);
            if (isGuid)
            {
                return result;
            }
            return null;
        }
    }
}
