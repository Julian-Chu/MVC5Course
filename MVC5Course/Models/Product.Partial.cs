namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Validations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [Required(ErrorMessage = "請輸入商品名稱({0})")]
        [DisplayName("商品名稱")]
        [商品名稱不能有Will字串Attribute(ErrorMessage ="商品名稱不能有Will字串2")]
        public string ProductName { get; set; }
        [Required]
        [Range(10, 9999999999, ErrorMessage = "({0}) 金額錯誤")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        [Range(0,999999999)]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
