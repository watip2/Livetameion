using Nop.Core;
using Nop.Core.Data;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Services.Common;
using Nop.Services.Events;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Services
{
    public class GroupDealService : IGroupDealService
    {
        #region Fields

        private readonly IRepository<GroupDeal> _groupDealRepo;
        private readonly IEventPublisher _eventPublisher;
        private IGenericAttributeService _genericAttributeService;
        private IVendorService _vendorService;
        private IRepository<GroupdealPicture> _groupdealPictureRepo;

        #endregion

        #region Ctor

        public GroupDealService(
            IRepository<GroupDeal> groupDealRepository,
            IEventPublisher eventPublisher,
            IGenericAttributeService genericAttributeService,
            IVendorService vendorService,
            IRepository<GroupdealPicture> groupdealPictureRepo)
        {
            this._groupDealRepo = groupDealRepository;
            this._eventPublisher = eventPublisher;
            this._genericAttributeService = genericAttributeService;
            _vendorService = vendorService;
            _groupdealPictureRepo = groupdealPictureRepo;
        }

        #endregion

        #region Methods

        public GroupDeal GetById(int groupDealId)
        {
            if (groupDealId == 0)
                return null;

            var groupdeal = _groupDealRepo.GetById(groupDealId);
            // getting generic attributes
            groupdeal.Name = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.Name, _genericAttributeService);
            groupdeal.AllowBackInStockSubscriptions = _groupDealRepo.GetById(groupDealId).GetAttribute<bool>(GroupDealAttributes.AllowBackInStockSubscriptions, _genericAttributeService);
            groupdeal.AvailableStartDateTimeUtc = _groupDealRepo.GetById(groupDealId).GetAttribute<DateTime>(GroupDealAttributes.AvailableStartDateTimeUtc, _genericAttributeService);
            groupdeal.AvailableEndDateTimeUtc = _groupDealRepo.GetById(groupDealId).GetAttribute<DateTime>(GroupDealAttributes.AvailableEndDateTimeUtc, _genericAttributeService);
            groupdeal.Country = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.Country, _genericAttributeService);
            groupdeal.StateOrProvince = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.StateOrProvince, _genericAttributeService);
            groupdeal.City = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.City, _genericAttributeService);
            groupdeal.DisplayStockAvailability = _groupDealRepo.GetById(groupDealId).GetAttribute<bool>(GroupDealAttributes.DisplayStockAvailability, _genericAttributeService);
            groupdeal.DisplayStockQuantity = _groupDealRepo.GetById(groupDealId).GetAttribute<bool>(GroupDealAttributes.DisplayStockQuantity, _genericAttributeService);
            groupdeal.GroupDealCost = _groupDealRepo.GetById(groupDealId).GetAttribute<decimal>(GroupDealAttributes.GroupDealCost, _genericAttributeService);
            groupdeal.MinStockQuantity = _groupDealRepo.GetById(groupDealId).GetAttribute<int>(GroupDealAttributes.MinStockQuantity, _genericAttributeService);
            groupdeal.OldPrice = _groupDealRepo.GetById(groupDealId).GetAttribute<decimal>(GroupDealAttributes.OldPrice, _genericAttributeService);
            groupdeal.Price = _groupDealRepo.GetById(groupDealId).GetAttribute<decimal>(GroupDealAttributes.Price, _genericAttributeService);
            groupdeal.SpecialPrice = _groupDealRepo.GetById(groupDealId).GetAttribute<decimal>(GroupDealAttributes.SpecialPrice, _genericAttributeService);
            groupdeal.StockQuantity = _groupDealRepo.GetById(groupDealId).GetAttribute<int>(GroupDealAttributes.StockQuantity, _genericAttributeService);
            groupdeal.ShortDescription = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.ShortDescription, _genericAttributeService);
            groupdeal.FullDescription = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.FullDescription, _genericAttributeService);

            return groupdeal;
        }

        public virtual void DeleteGroupdeal(GroupDeal groupDeal)
        {
            if (groupDeal == null)
                throw new ArgumentNullException("groupDeal");

            groupDeal.Deleted = true;
            UpdateGroupdeal(groupDeal);
        }
        
        //public IPagedList<GroupDeal> GetAllGroupdeals(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        //{
        //    var query = _groupDealRepo.Table;
        //    if (!String.IsNullOrWhiteSpace(name))
        //        query = query.Where(gd => gd.Name.Contains(name));
        //    if (!showHidden)
        //        query = query.Where(gd => gd.Active);
        //    query = query.Where(gd => !gd.Deleted);
        //    query = query.OrderBy(gd => gd.DisplayOrder).ThenBy(v => v.Name);

        //    var groupdeals = new PagedList<GroupDeal>(query, pageIndex, pageSize);
        //    return groupdeals;
        //}

        public IEnumerable<GroupDeal> GetAllGroupdeals()
        {
            var groupDeals = _groupDealRepo.Table.ToList();
            List<GroupDeal> _groupdeals = new List<GroupDeal>();
            foreach (var groupDeal in groupDeals)
            {
                _groupdeals.Add(this.GetById(groupDeal.Id));
            }

            return _groupdeals.Where(gd => !gd.Deleted);
        }

        public void InsertGroupDeal(GroupDeal groupDeal)
        {
            if (groupDeal == null)
                throw new ArgumentNullException("GroupDeal");
            
            _groupDealRepo.Insert(groupDeal);
            SaveGenericAttributes(groupDeal);

            //event notification
            _eventPublisher.EntityInserted(groupDeal);
        }

        public IEnumerable<GroupDeal> GetAllGroupDealsByVendorId(int vendorId)
        {
            var vendor = _vendorService.GetVendorById(vendorId);
            var _groupDeals = _groupDealRepo.Table.Where(gd => gd.VendorId == vendorId);

            var attributes = _genericAttributeService.GetAttributesForEntity(vendorId, "GroupDeal");
            var gdid = vendor.GetAttribute<int>("GroupDealId");

            List<GroupDeal> groupdeals = new List<GroupDeal>();
            foreach (var _groupDeal in _groupDeals)
            {
                groupdeals.Add(this.GetById(_groupDeal.Id));
            }

            return groupdeals;
        }

        private void SaveGenericAttributes(GroupDeal groupDeal)
        {
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.Name, groupDeal.Name);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.AllowBackInStockSubscriptions, groupDeal.AllowBackInStockSubscriptions);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.AvailableStartDateTimeUtc, groupDeal.AvailableStartDateTimeUtc);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.AvailableEndDateTimeUtc, groupDeal.AvailableEndDateTimeUtc);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.Country, groupDeal.Country);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.StateOrProvince, groupDeal.StateOrProvince);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.City, groupDeal.City);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.DisplayStockAvailability, groupDeal.DisplayStockAvailability);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.DisplayStockQuantity, groupDeal.DisplayStockQuantity);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.GroupDealCost, groupDeal.GroupDealCost);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.MinStockQuantity, groupDeal.MinStockQuantity);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.OldPrice, groupDeal.OldPrice);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.Price, groupDeal.Price);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.SpecialPrice, groupDeal.SpecialPrice);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.StockQuantity, groupDeal.StockQuantity);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.ShortDescription, groupDeal.ShortDescription);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.FullDescription, groupDeal.FullDescription);
        }

        public IList<GroupdealPicture> GetGroupdealPicturesByGroupdealId(int GroupdealId)
        {
            var query = from gp in _groupdealPictureRepo.Table
                        where gp.GroupdealId == GroupdealId
                        orderby gp.DisplayOrder
                        select gp;
            var groupdealPictures = query.ToList();
            return groupdealPictures;
        }

        public GroupdealPicture GetGroupdealPictureById(int groupdealPictureId)
        {
            if (groupdealPictureId == 0)
                return null;

            return _groupdealPictureRepo.GetById(groupdealPictureId);
        }

        public void InsertGroupdealPicture(GroupdealPicture groupdealPicture)
        {
            if (groupdealPicture == null)
                throw new ArgumentNullException("groupdealPicture");

            _groupdealPictureRepo.Insert(groupdealPicture);

            //event notification
            _eventPublisher.EntityInserted(groupdealPicture);
        }

        public virtual void UpdateGroupdeal(GroupDeal groupDeal)
        {
            if (groupDeal == null)
                throw new ArgumentNullException("groupDeal");

            var gd = this.GetById(groupDeal.Id);
            gd.Id = groupDeal.Id;
            gd.CreatedOnUtc = groupDeal.CreatedOnUtc;
            gd.UpdatedOnUtc = groupDeal.UpdatedOnUtc;
            gd.VendorId = groupDeal.VendorId;
            gd.Deleted = groupDeal.Deleted;
            gd.Active = groupDeal.Active;
            gd.DisplayOrder = groupDeal.DisplayOrder;
            gd.SeName = groupDeal.SeName;
            gd.ShowOnHomePage = groupDeal.ShowOnHomePage;
            gd.Published = groupDeal.Published;
            gd.CouponCode = groupDeal.CouponCode;
            gd.SpecialPriceStartDateTimeUtc = groupDeal.SpecialPriceStartDateTimeUtc;
            gd.SpecialPriceEndDateTimeUtc = groupDeal.SpecialPriceEndDateTimeUtc;
            _groupDealRepo.Update(groupDeal);
            
            SaveGenericAttributes(groupDeal);
            
            //event notification
            _eventPublisher.EntityUpdated(groupDeal);
        }

        public void UpdateGroupdealPicture(GroupdealPicture groupdealPicture)
        {
            if (groupdealPicture == null)
                throw new ArgumentNullException("productPicture");

            _groupdealPictureRepo.Update(groupdealPicture);

            //event notification
            _eventPublisher.EntityUpdated(groupdealPicture);
        }

        public void DeleteGroupdealPicture(GroupdealPicture groupdealPicture)
        {
            if (groupdealPicture == null)
                throw new ArgumentNullException("productPicture");

            _groupdealPictureRepo.Delete(groupdealPicture);

            //event notification
            _eventPublisher.EntityDeleted(groupdealPicture);
        }

        public string GenerateGroupdealCouponCode()
        {
            int length = 13;
            string result = Guid.NewGuid().ToString();
            if (result.Length > length)
                result = result.Substring(0, length);
            return result;
        }

        #endregion
    }
}
