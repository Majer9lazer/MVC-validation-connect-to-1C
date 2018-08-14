using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Company.Register.Lib.Model;

namespace MVC_validation_connect_to_1C.Models
{
    public class LocaUser
    {
        public LocaUser()
        {
            AddressLegal.ContryId = 1;
            AddressLegal.CityId = 3;
            AddressPhysical.ContryId = 1;
            AddressPhysical.CityId = 3;
            Phone p = new Phone();
            
        }
        public int UserId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        public string RePassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailElInvoice { get; set; }

        [Range(1, 2, ErrorMessage = "7.	Поле «IsLegalEntity» имеет всего 2 значения ")]
        public int IsLegalEntity { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "БИН/ИИН Должен быть = 12")]
        public string Bin { get; set; }
        [StringLength(2, ErrorMessage = "5.	Поле «Kbe», должно быть всегда 2-х значным числом")]
        public string Kbe { get; set; }

        public string CertSeries { get; set; }

        public string CertNumber { get; set; }

        public DateTime? CertDateIssue { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string NameOrganization { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string User1CGuid { get; set; }

        //public virtual ICollection<Company.Register.Lib.Model.Address> Address { get; set; }

        //public virtual ICollection<Company.Register.Lib.Model.BankDetail> BankDetail { get; set; }

        //public virtual ICollection<Company.Register.Lib.Model.Phone> Phone { get; set; }

        public Phone[] ContactNumbers { get; set; } = new Phone[1];

        public Address AddressPhysical { get; set; } = new Address();

        public Address AddressLegal { get; set; } = new Address();

        public BankDetail[] BankDetails { get; set; } = new BankDetail[1];

        public bool RememberMe { get; set; } = false;

        public static explicit operator UserAccount(LocaUser obj)
        {

            UserAccount userAccount = new UserAccount
            {
                UserId = obj.UserId,
                Login = obj.Login,
                Password = obj.Password,
                Email = obj.Email,
                EmailElInvoice = obj.EmailElInvoice,
                IsLegalEntity = obj.IsLegalEntity,
                Bin = obj.Bin,
                Kbe = obj.Kbe,
                CertSeries = obj.CertSeries,
                CertNumber = obj.CertNumber,
                CertDateIssue = obj.CertDateIssue,
                Status = obj.Status,
                CreateDate = obj.CreateDate,
                UpdateDate = obj.UpdateDate,
                NameOrganization = obj.NameOrganization,
                Surname = obj.Surname,
                Name = obj.Name,
                Patronymic = obj.Patronymic,
                User1CGuid = obj.User1CGuid,
                ContactNumbers = obj.ContactNumbers,
                AddressPhysical = obj.AddressPhysical,
                BankDetails = obj.BankDetails,
                AddressLegal = obj.AddressLegal,
                RememberMe = obj.RememberMe,
                Address = new List<Address>() { obj.AddressLegal,obj.AddressPhysical}
            };

            return userAccount;
        }
    }
}