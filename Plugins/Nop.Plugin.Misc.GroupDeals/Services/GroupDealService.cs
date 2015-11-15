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

        public GroupDeal GetGroupDealById(int groupDealId)
        {
            if (groupDealId == 0)
                return null;
            
            return new GroupDeal
            {
                Id = _groupDealRepo.GetById(groupDealId).Id,
                AttributeSetId = _groupDealRepo.GetById(groupDealId).AttributeSetId,
                CreatedOnUtc = _groupDealRepo.GetById(groupDealId).CreatedOnUtc,
                UpdatedOnUtc = _groupDealRepo.GetById(groupDealId).UpdatedOnUtc,
                VendorId = _groupDealRepo.GetById(groupDealId).VendorId,
                Deleted = _groupDealRepo.GetById(groupDealId).Deleted,
                Active = _groupDealRepo.GetById(groupDealId).Active,
                DisplayOrder = _groupDealRepo.GetById(groupDealId).DisplayOrder,
                // getting generic attributes
                Name = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.Name, _genericAttributeService),
                AllowBackInStockSubscriptions = _groupDealRepo.GetById(groupDealId).GetAttribute<bool>(GroupDealAttributes.AllowBackInStockSubscriptions, _genericAttributeService),
                AvailableStartDateTimeUtc = _groupDealRepo.GetById(groupDealId).GetAttribute<DateTime>(GroupDealAttributes.AvailableStartDateTimeUtc, _genericAttributeService),
                AvailableEndDateTimeUtc = _groupDealRepo.GetById(groupDealId).GetAttribute<DateTime>(GroupDealAttributes.AvailableEndDateTimeUtc, _genericAttributeService),
                Country = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.Country, _genericAttributeService),
                StateOrProvince = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.StateOrProvince, _genericAttributeService),
                City = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.City, _genericAttributeService),
                DisplayStockAvailability = _groupDealRepo.GetById(groupDealId).GetAttribute<bool>(GroupDealAttributes.DisplayStockAvailability, _genericAttributeService),
                DisplayStockQuantity = _groupDealRepo.GetById(groupDealId).GetAttribute<bool>(GroupDealAttributes.DisplayStockQuantity, _genericAttributeService),
                GroupDealCost = _groupDealRepo.GetById(groupDealId).GetAttribute<decimal>(GroupDealAttributes.GroupDealCost, _genericAttributeService),
                MinStockQuantity = _groupDealRepo.GetById(groupDealId).GetAttribute<int>(GroupDealAttributes.MinStockQuantity, _genericAttributeService),
                OldPrice = _groupDealRepo.GetById(groupDealId).GetAttribute<decimal>(GroupDealAttributes.OldPrice, _genericAttributeService),
                Price = _groupDealRepo.GetById(groupDealId).GetAttribute<decimal>(GroupDealAttributes.Price, _genericAttributeService),
                SpecialPrice = _groupDealRepo.GetById(groupDealId).GetAttribute<decimal>(GroupDealAttributes.SpecialPrice, _genericAttributeService),
                StockQuantity = _groupDealRepo.GetById(groupDealId).GetAttribute<int>(GroupDealAttributes.StockQuantity, _genericAttributeService),
            };
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
                _groupdeals.Add(this.GetGroupDealById(groupDeal.Id));
            }

            return _groupdeals.Where(gd => !gd.Deleted);
        }

        public void InsertGroupDeal(GroupDeal groupDeal)
        {
            if (groupDeal == null)
                throw new ArgumentNullException("GroupDeal");

            var gd = new GroupDeal();
            gd.Id = groupDeal.Id;
            gd.AttributeSetId = groupDeal.AttributeSetId;
            gd.CreatedOnUtc = groupDeal.CreatedOnUtc;
            gd.UpdatedOnUtc = groupDeal.UpdatedOnUtc;
            gd.VendorId = groupDeal.VendorId;
            gd.Deleted = groupDeal.Deleted;
            gd.Active = groupDeal.Active;
            gd.DisplayOrder = groupDeal.DisplayOrder;
            _groupDealRepo.Insert(gd);

            groupDeal.Id = gd.Id;
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
                groupdeals.Add(this.GetGroupDealById(_groupDeal.Id));
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

        public virtual void UpdateGroupdeal(GroupDeal groupDeal)
        {
            if (groupDeal == null)
                throw new ArgumentNullException("groupDeal");

            _groupDealRepo.Update(new GroupDeal
            {
                Id = groupDeal.Id,
                AttributeSetId = groupDeal.AttributeSetId,
                CreatedOnUtc = groupDeal.CreatedOnUtc,
                UpdatedOnUtc = groupDeal.UpdatedOnUtc,
                VendorId = groupDeal.VendorId,
                Deleted = groupDeal.Deleted,
                Active = groupDeal.Active,
                DisplayOrder = groupDeal.DisplayOrder
            });
            SaveGenericAttributes(groupDeal);

            //event notification
            _eventPublisher.EntityUpdated(groupDeal);
        }

        #endregion
    }
}
