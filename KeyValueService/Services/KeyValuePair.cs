using System.ComponentModel.DataAnnotations;

namespace KeyValueService.Services
{
    public class KeyValuePairEntity
    {
        [Key]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
