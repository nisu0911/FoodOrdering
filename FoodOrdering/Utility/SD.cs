using FoodOrdering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrdering.Utility
{
    public class SD
    {
        public const string DefaultFoodImage = "defaultFood.png";

        public const string ManagerUser = "Manager";
        public const string FrontDeskUser = "FrontDesk";
        public const string CustomerEndUser = "Customer";

        public static string ssShoppingCartCount = "ssCartCount";
        public static string ssCouponCode = "ssCouponCode";

        public const string StatusSubmitted = "Submitted";
        public const string StatusInProcess = "Being Prepared";
        public const string StatusReady = "Ready for Pickup";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusRejected = "Rejected";


        public static double DiscountedPrice(Coupon couponDb, double OrginalOrderTotal)
        {
            if (couponDb == null)
            {
                return OrginalOrderTotal;
            }
            else
            {
                if (couponDb.MinimumAmount > OrginalOrderTotal)
                {
                    return OrginalOrderTotal;
                }
                else
                {
                    // check coupon type
                    if (Convert.ToInt32(couponDb.CouponType) == (int)Coupon.ECouponType.Rupees)
                    {
                        // Rs off Total
                        return Math.Round(OrginalOrderTotal - couponDb.Discount, 2);
                    }
                    if (Convert.ToInt32(couponDb.CouponType) == (int)Coupon.ECouponType.Percent)
                    {
                        // % off Total
                        return Math.Round(OrginalOrderTotal - (OrginalOrderTotal * couponDb.Discount/100), 2);
                    }
                }
            }
            return OrginalOrderTotal;
        }
    }
}
