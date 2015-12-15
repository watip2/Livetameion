using Nop.Web.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.ViewModels
{
    public static class VendorRegisterModelExtensions
    {
        public static RegisterModel ToRegisterModel(this VendorRegisterViewModel vrvm, RegisterModel registerModel)
        {
            registerModel.FirstName = "Dummy";
            registerModel.LastName = "Dummy";
            registerModel.Email = vrvm.Email;
            registerModel.Password = vrvm.Password;
            registerModel.ConfirmPassword = vrvm.ConfirmPassword;

            return registerModel;
        }
    }
}
