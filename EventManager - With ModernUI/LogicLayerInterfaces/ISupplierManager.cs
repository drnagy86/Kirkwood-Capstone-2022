using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ISupplierManager
    {
        List<Supplier> RetrieveActiveSuppliers();
        List<Supplier> RetrieveUnapprovedSuppliers();
        List<Supplier> RetrieveSuppliersByUserID(int userID);
        List<Reviews> RetrieveSupplierReviewsBySupplierID(int supplierID);
        int CreateSupplierReview(Reviews review);
        List<string> RetrieveSupplierTagsBySupplierID(int supplierID);
        List<string> RetrieveSupplierImagesBySupplierID(int supplierID);
        List<Availability> RetrieveSupplierAvailabilityBySupplierIDAndDate(int supplierID, DateTime date);
        List<DateTime> SupplierAvailabilityForNextThreeMonths(int supplierID);
        List<AvailabilityVM> RetrieveSupplierAvailabilityBySupplierID(int supplierID);
        List<Availability> RetrieveSupplierAvailabilityExceptionBySupplierID(int supplierID);
        Supplier RetrieveSupplierBySupplierID(int supplierID);
        bool ApproveSupplier(int supplierID);
        bool DisapproveSupplier(int supplierID);
        bool RequeueSupplier(int supplierID);
        int CreateSupplier(Supplier supplier);
        bool EditSupplier(Supplier oldSupplier, Supplier newSupplier);
        int CreateSupplierAvailability(Availability availability);
    }
}
