using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Company.Register.Lib.Model;

namespace MVC_validation_connect_to_1C.Models
{
    public class IsLegalAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            if (value is LocaUser user)
            {

                switch (user.IsLegalEntity)
                {
                    case 1:
                        {
                            if (!string.IsNullOrEmpty(user.NameOrganization))
                            {

                                user.Surname = null;
                                user.Name = null;
                                user.Patronymic = null;
                                if (!string.IsNullOrEmpty(user.AddressLegal.House) ||
                                    !string.IsNullOrEmpty(user.AddressLegal.Street))
                                {
                                    return true;

                                }
                                else
                                {
                                    ErrorMessage = "Дом и улица должны быть заполнены";
                                    return false;
                                }

                            }
                            else
                            {
                                ErrorMessage = "Поле NameOrganization обязательно к заполнению.";
                                return false;
                            }
                        }
                    case 2:
                        {
                            if (!string.IsNullOrEmpty(user.AddressLegal.House) ||
                                !string.IsNullOrEmpty(user.AddressLegal.Street))
                            {
                                if (!string.IsNullOrEmpty(user.Surname))
                                {
                                    if (!string.IsNullOrEmpty(user.Name))
                                    {
                                        if (!string.IsNullOrEmpty(user.Patronymic))
                                        {
                                            return true;
                                        }
                                        ErrorMessage = "Поле Patronymics является обязательным для заполнения";
                                        return false;
                                    }

                                    ErrorMessage = "Поле Name является обязательным для заполнения";
                                    return false;
                                }

                                ErrorMessage = "Поле Surname является обязательным для заполнения";
                                return false;

                            }

                            ErrorMessage = "Поле дом и улица должны быть заполненными";
                            return false;
                        }
                    default:
                        {
                            ErrorMessage = "В поле лиц вы должны ввести 1 или 2. Другого не дано.";
                            return false;
                        }
                }
            }
            else
            {
                ErrorMessage = "Пришел не тот вид объекта";
                return false;
            }
        }
    }
    [IsLegal]
    public class LocaUser
    {
        public LocaUser()
        {
            AddressLegal.ContryId = 1;
            AddressLegal.CityId = 3;
            AddressPhysical.ContryId = 1;
            AddressPhysical.CityId = 3;


        }

        public Phone PhoneNumber { get; set; }

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
        public int IsLegalEntity { get; set; }
        [Required]
        [StringLength(12, ErrorMessage = "БИН/ИИН Должен быть = 12")]
        public string Bin { get; set; }

        [StringLength(2, ErrorMessage = "Поле «Kbe», должно быть всегда 2-х значным числом")]
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
        [Required]
        public string Bik { get; set; }

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
                Address = new List<Address>() { obj.AddressLegal, obj.AddressPhysical }
            };

            return userAccount;
        }
    }
}