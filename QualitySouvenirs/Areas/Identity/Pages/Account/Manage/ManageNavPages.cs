using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace QualitySouvenirs.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string ChangePassword => "ChangePassword";

        public static string Orders => "Orders";

        public static string Categories => "Categories";

        public static string Souvenirs => "Souvenirs";

        public static string Suppliers => "Suppliers";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string OrdersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Orders);

        public static string CategoriesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Categories);

        public static string SouvenirsClass(ViewContext viewContext) => PageNavClass(viewContext, Souvenirs);

        public static string SuppliersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Suppliers);
    
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            if (viewContext.ActionDescriptor.DisplayName.StartsWith("/Account/Manage/Orders"))
            {
                activePage = Orders;
            }
            else if (viewContext.ActionDescriptor.DisplayName.StartsWith("/Account/Manage/Categories"))
            {
                activePage = Categories;
            }
            else if (viewContext.ActionDescriptor.DisplayName.StartsWith("/Account/Manage/Souvenirs"))
            {
                activePage = Souvenirs;
            }
            else if (viewContext.ActionDescriptor.DisplayName.StartsWith("/Account/Manage/Suppliers"))
            {
                activePage = Suppliers;
            }
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
