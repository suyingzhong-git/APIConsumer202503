using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APIConsumer.ViewModels
{
    public class RainfallViewModel
    {
  
            [Key]
            public long Id { get; set; }

        [DisplayName("X Ref")]
        public int Xref { get; set; }
            public int Yref { get; set; }
            public DateTime RFDate { get; set; }
            public int RFValue { get; set; }
            public bool? IsActive { get; set; }
            public DateTime CreatedOn { get; set; }

            public string? Comment { get; set; }

    }
}
