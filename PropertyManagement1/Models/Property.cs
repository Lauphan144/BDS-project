using System.ComponentModel.DataAnnotations;
namespace PropertyManagement1.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    public partial class Property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property()
        {
            this.Full_Contract = new HashSet<Full_Contract>();
            this.Installment_Contract = new HashSet<Installment_Contract>();
        }

        public int ID { get; set; }
        [Display(Name = "Code")]
        public string Property_Code { get; set; }
        [Display(Name = "Tên Bất Động Sản")]
        public string Property_Name { get; set; }
        [Display(Name = "Loại Bất Động Sản")]
        public int Property_Type_ID { get; set; }
        [Display(Name = "Mô Tả")]
        public string Description { get; set; }
        public int District_ID { get; set; }
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }
        [Display(Name = "Diện Tích")]
        [Range(1,1000)]
        public Nullable<int> Area { get; set; }
        [Range(1, 1000)]
        [Display(Name = "Phòng Ngủ")]
        public Nullable<int> Bed_Room { get; set; }
        [Display(Name = "Phòng Tắm")]
        [Range(1, 1000)]
        public Nullable<int> Bath_Room { get; set; }
        [Display(Name = "Price")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]

        [UIHint("Currency")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> Price { get; set; }
        [Display(Name = "Tỷ Lệ Trả Góp")]
        public Nullable<double> Installment_Rate { get; set; }
        public string Avatar { get; set; }

        public string Album { get; set; }
        public int Property_Status_ID { get; set; }

        public virtual District District { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Full_Contract> Full_Contract { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Installment_Contract> Installment_Contract { get; set; }
        public virtual Property_Status Property_Status { get; set; }
        public virtual Property_Type Property_Type { get; set; }
    }
}