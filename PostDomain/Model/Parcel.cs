using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostDomain.Model;

public partial class Parcel : Entity
{
    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public int? SenderBranchId { get; set; }

    public int? ReceiverBranchId { get; set; }

    public int? DeliveryCityId { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Адреса доставки")]
    public string? DeliveryAddress { get; set; }

    public int TariffId { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Вага")]
    public double Weight { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Статус")]
    [Column("status")]
    public ParcelStatus Status { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Місто доставки")]
    public virtual City? DeliveryCity { get; set; }

    public virtual ICollection<ParcelCourier> ParcelCouriers { get; set; } = new List<ParcelCourier>();

    public virtual ParcelDimension? ParcelDimension { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Отримувач")]
    public virtual Customer Receiver { get; set; } = null!;

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Відділення отримувача")]
    public virtual Branch? ReceiverBranch { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Відправник")]
    public virtual Customer Sender { get; set; } = null!;

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Відділення відправника")]
    public virtual Branch? SenderBranch { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Тариф")]
    public virtual Tariff Tariff { get; set; } = null!;
}
