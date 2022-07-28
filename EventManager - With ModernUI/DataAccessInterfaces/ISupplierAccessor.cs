using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface ISupplierAccessor
    {
        List<Supplier> SelectActiveSuppliers();
        List<Supplier> SelectUnapprovedSuppliers();
        List<Supplier> SelectSuppliersByUserID(int userID);
        Supplier SelectSupplierBySupplierID(int supplierID);
        List<Reviews> SelectSupplierReviewsBySupplierID(int supplierID);
        int InsertSupplierReview(Reviews review);
        List<string> SelectSupplierTagsBySupplierID(int supplierID);
        List<string> SelectSupplierImagesBySupplierID(int supplierID);
        List<Availability> SelectSupplierAvailabilityBySupplierIDAndDate(int supplierID, DateTime date);
        List<Availability> SelectSupplierAvailabilityExceptionBySupplierIDAndDate(int supplierID, DateTime date);

        List<DateTime> SelectSupplierAvailabilityForNextThreeMonths(int supplierID);
        List<AvailabilityVM> SelectSupplierAvailabilityBySupplierID(int supplierID);
        List<Availability> SelectSupplierAvailabilityExceptionBySupplierID(int supplierID);
        int ApproveSupplier(int supplierID);
        int DisapproveSupplier(int supplierID);
        int RequeueSupplier(int supplierID);
        int InsertSupplier(Supplier supplier);
        int UpdateSupplier(Supplier oldSupplier, Supplier newSupplier);
        int InsertSupplierAvailability(Availability availability);
    }
}
