using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using Nop.Data;
using Nop.Plugin.Misc.GroupDeals.Extensions;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Services
{
    public class GroupDealService : ProductService, IGroupDealService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        private const string PRODUCTS_BY_ID_KEY = "Nop.product.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PRODUCTS_PATTERN_KEY = "Nop.product.";
        #endregion

        #region Fields

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<RelatedProduct> _relatedProductRepository;
        private readonly IRepository<CrossSellProduct> _crossSellProductRepository;
        private readonly IRepository<TierPrice> _tierPriceRepository;
        private readonly IRepository<LocalizedProperty> _localizedPropertyRepository;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IRepository<ProductPicture> _productPictureRepository;
        private readonly IRepository<ProductSpecificationAttribute> _productSpecificationAttributeRepository;
        private readonly IRepository<ProductReview> _productReviewRepository;
        private readonly IRepository<ProductWarehouseInventory> _productWarehouseInventoryRepository;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly ILanguageService _languageService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly LocalizationSettings _localizationSettings;
        private readonly CommonSettings _commonSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly IEventPublisher _eventPublisher;
        private readonly IAclService _aclService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IRepository<Product> _productRepo;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IRepository<GroupDeal> _groupDealRepo;
        private readonly IRepository<GroupDeal> _groupDealRepository;
        private readonly IVendorService _vendorService;
        private readonly IRepository<GroupdealPicture> _groupdealPictureRepo;
        private readonly IRepository<Product> _groupDealProductRepo;
        private readonly IRepository<ProductPicture> _groupDealProductPictureRepo;
        private readonly IRepository<GenericAttribute> _genericAttributeRepo;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="productRepository">Product repository</param>
        /// <param name="relatedProductRepository">Related product repository</param>
        /// <param name="crossSellProductRepository">Cross-sell product repository</param>
        /// <param name="tierPriceRepository">Tier price repository</param>
        /// <param name="localizedPropertyRepository">Localized property repository</param>
        /// <param name="aclRepository">ACL record repository</param>
        /// <param name="storeMappingRepository">Store mapping repository</param>
        /// <param name="productPictureRepository">Product picture repository</param>
        /// <param name="productSpecificationAttributeRepository">Product specification attribute repository</param>
        /// <param name="productReviewRepository">Product review repository</param>
        /// <param name="productWarehouseInventoryRepository">Product warehouse inventory repository</param>
        /// <param name="productAttributeService">Product attribute service</param>
        /// <param name="productAttributeParser">Product attribute parser service</param>
        /// <param name="languageService">Language service</param>
        /// <param name="workflowMessageService">Workflow message service</param>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="dbContext">Database Context</param>
        /// <param name="workContext">Work context</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="localizationSettings">Localization settings</param>
        /// <param name="commonSettings">Common settings</param>
        /// <param name="catalogSettings">Catalog settings</param>
        /// <param name="eventPublisher">Event published</param>
        /// <param name="aclService">ACL service</param>
        /// <param name="storeMappingService">Store mapping service</param>
        public GroupDealService(ICacheManager cacheManager,
            IRepository<Product> productRepository,
            IRepository<RelatedProduct> relatedProductRepository,
            IRepository<CrossSellProduct> crossSellProductRepository,
            IRepository<TierPrice> tierPriceRepository,
            IRepository<ProductPicture> productPictureRepository,
            IRepository<LocalizedProperty> localizedPropertyRepository,
            IRepository<AclRecord> aclRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IRepository<ProductSpecificationAttribute> productSpecificationAttributeRepository,
            IRepository<ProductReview> productReviewRepository,
            IRepository<ProductWarehouseInventory> productWarehouseInventoryRepository,
            IProductAttributeService productAttributeService,
            IProductAttributeParser productAttributeParser,
            ILanguageService languageService,
            IWorkflowMessageService workflowMessageService,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IWorkContext workContext,
            IStoreContext storeContext,
            LocalizationSettings localizationSettings,
            CommonSettings commonSettings,
            CatalogSettings catalogSettings,
            IEventPublisher eventPublisher,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            IRepository<Product> productRepo,
            IGenericAttributeService genericAttributeService,
            IRepository<GroupDeal> groupDealRepository,
            IVendorService vendorService,
            IRepository<GroupdealPicture> groupdealPictureRepo,
            IRepository<Product> groupDealProductRepo,
            IRepository<ProductPicture> groupDealProductPictureRepo,
            IRepository<GenericAttribute> genericAttributeRepo)
            : base(cacheManager,
                productRepository,
                relatedProductRepository,
                crossSellProductRepository,
                tierPriceRepository,
                productPictureRepository,
                localizedPropertyRepository,
                aclRepository,
                storeMappingRepository,
                productSpecificationAttributeRepository,
                productReviewRepository,
                productWarehouseInventoryRepository,
                productAttributeService,
                productAttributeParser,
                languageService,
                workflowMessageService,
                dataProvider,
                dbContext,
                workContext,
                storeContext,
                localizationSettings,
                commonSettings,
                catalogSettings,
                eventPublisher,
                aclService,
                storeMappingService)
        {
            this._cacheManager = cacheManager;
            this._productRepository = productRepository;
            this._relatedProductRepository = relatedProductRepository;
            this._crossSellProductRepository = crossSellProductRepository;
            this._tierPriceRepository = tierPriceRepository;
            this._productPictureRepository = productPictureRepository;
            this._localizedPropertyRepository = localizedPropertyRepository;
            this._aclRepository = aclRepository;
            this._storeMappingRepository = storeMappingRepository;
            this._productSpecificationAttributeRepository = productSpecificationAttributeRepository;
            this._productReviewRepository = productReviewRepository;
            this._productWarehouseInventoryRepository = productWarehouseInventoryRepository;
            this._productAttributeService = productAttributeService;
            this._productAttributeParser = productAttributeParser;
            this._languageService = languageService;
            this._workflowMessageService = workflowMessageService;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._localizationSettings = localizationSettings;
            this._commonSettings = commonSettings;
            this._catalogSettings = catalogSettings;
            this._eventPublisher = eventPublisher;
            this._aclService = aclService;
            this._storeMappingService = storeMappingService;
            this._productRepo = productRepo;
            this._genericAttributeService = genericAttributeService;
            this._genericAttributeService = genericAttributeService;
            this._groupDealRepository = groupDealRepository;
            this._vendorService = vendorService;
            this._groupdealPictureRepo = groupdealPictureRepo;
            this._groupDealProductRepo = groupDealProductRepo;
            this._groupDealProductPictureRepo = groupDealProductPictureRepo;
            this._genericAttributeRepo = genericAttributeRepo;
        }
        #endregion

        #region Methods

        public GroupDeal GetGroupDealById(int groupDealId)
        {
            if (groupDealId == 0)
                return null;

            var groupdeal = _groupDealRepo.GetById(groupDealId);
            // getting generic attributes
            groupdeal.Country = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.Country, _genericAttributeService);
            groupdeal.StateOrProvince = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.StateOrProvince, _genericAttributeService);
            groupdeal.City = _groupDealRepo.GetById(groupDealId).GetAttribute<string>(GroupDealAttributes.City, _genericAttributeService);
            
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
                _groupdeals.Add(this.GetGroupDealById(groupDeal.Id));
            }

            return _groupdeals.Where(gd => !gd.Deleted);
        }

        public void InsertGroupDeal(GroupDeal groupDeal)
        {
            if (groupDeal == null)
                throw new ArgumentNullException("groupDeal");
            
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
                groupdeals.Add(this.GetGroupDealById(_groupDeal.Id));
            }

            return groupdeals;
        }

        private void SaveGenericAttributes(GroupDeal groupDeal)
        {
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.Country, groupDeal.Country);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.StateOrProvince, groupDeal.StateOrProvince);
            _genericAttributeService.SaveAttribute(groupDeal, GroupDealAttributes.City, groupDeal.City);
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

            var gd = this.GetGroupDealById(groupDeal.Id);
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

        ///////////////////////////////////////////////////////////////////////////////////////
        public void InsertGroupDealProduct(Product groupDealProduct)
        {
            if (groupDealProduct == null)
                throw new ArgumentNullException("GroupDeal");

            _productRepo.Insert(groupDealProduct);
            //SaveGroupDealProductGenericAttributes(groupDealProduct);

            //event notification
            _eventPublisher.EntityInserted(groupDealProduct);
        }

        private void SaveGroupDealProductGenericAttributes(Product groupDealProduct)
        {
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.Active, true);
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.SeName, "dummy-SeName");
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.CouponCode, "12345");
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.Country, "Pakistan");
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.StateOrProvince, "KPK");
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.City, "Kamra");
        }

        public Product GetGroupDealProductById(int groupDealProductId)
        {
            if (groupDealProductId == 0)
                return null;
            
            var groupDealGenericEntity = _genericAttributeRepo.Table.FirstOrDefault(ga => ga.EntityId == groupDealProductId && ga.KeyGroup == "Product");
            if (groupDealGenericEntity == null) return null;

            return _productRepo.GetById(groupDealGenericEntity.EntityId);
            // getting generic attributes
            //groupdeal.Country = _groupDealRepo.GetById(groupDealProductId).GetAttribute<string>(GroupDealAttributes.Country, _genericAttributeService);
            //groupdeal.StateOrProvince = _groupDealRepo.GetById(groupDealProductId).GetAttribute<string>(GroupDealAttributes.StateOrProvince, _genericAttributeService);
            //groupdeal.City = _groupDealRepo.GetById(groupDealProductId).GetAttribute<string>(GroupDealAttributes.City, _genericAttributeService);
        }

        public void DeleteGroupDealProduct(Product groupDealProduct)
        {
            if (groupDealProduct == null)
                throw new ArgumentNullException("groupDeal");

            groupDealProduct.Deleted = true;
            UpdateGroupDealProduct(groupDealProduct);
        }

        public IEnumerable<GroupDeal> GetAllGroupDealProductsByVendorId(int vendorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllGroupDealProducts()
        {
            var groupDealGenericEntities = _genericAttributeRepo.Table.Where(ga => ga.KeyGroup.Equals("Product") && ga.Key.Equals(GroupDealAttributes.IsGroupDeal) && ga.Value.Equals("True")).ToList();
            var products = new List<Product>();
            foreach (var entity in groupDealGenericEntities)
            {
                products.Add(_productRepo.GetById(entity.EntityId));
            }

            return products;
        }

        public void UpdateGroupDealProduct(Product groupDealProduct)
        {
            if (groupDealProduct == null)
                throw new ArgumentNullException("groupDealProduct");

            _productRepo.Update(groupDealProduct);
            
            //event notification
            _eventPublisher.EntityUpdated(groupDealProduct);
        }

        public IList<GroupdealPicture> GetGroupDealProductPicturesByGroupDealId(int groupDealProductId)
        {
            throw new NotImplementedException();
        }

        public GroupdealPicture GetGroupDealProductPictureById(int groupDealPictureId)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroupDealProductPicture(GroupdealPicture groupDealProductPicture)
        {
            throw new NotImplementedException();
        }

        public void DeleteGroupDealProductPicture(GroupdealPicture groupDealPicture)
        {
            throw new NotImplementedException();
        }

        public void InsertGroupDealProductPicture(GroupdealPicture groupDealProductPicture)
        {
            throw new NotImplementedException();
        }

        public string GenerateGroupDealProductCouponCode()
        {
            int length = 13;
            string result = Guid.NewGuid().ToString();
            if (result.Length > length)
                result = result.Substring(0, length);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name">GroupDeal Name (Title)</param>
        /// <param name="Price">GroupDeal Price</param>
        /// <returns>GroupDeal Id</returns>
        public int CreateGroupDealProduct(string Name, decimal Price)
        {
            var groupDealProduct = new Product
            {
                DisplayOrder = 1,
                ShortDescription = "short description",
                FullDescription = "full description",
                Published = true,
                DisplayStockQuantity = true,
                StockQuantity = 1,
                Price = Price,
                SpecialPrice = Price - (0.40m * Price),
                Name = Name + " group deal",
                VisibleIndividually = true,
                OrderMinimumQuantity = 1,
                OrderMaximumQuantity = int.MaxValue,
                AllowCustomerReviews = true,
                ProductType = ProductType.SimpleProduct,

                // datetime fields
                AvailableStartDateTimeUtc = DateTime.UtcNow,
                AvailableEndDateTimeUtc = DateTime.UtcNow.AddYears(1),
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow,
                SpecialPriceStartDateTimeUtc = DateTime.UtcNow,
                SpecialPriceEndDateTimeUtc = DateTime.UtcNow.AddYears(1)
            };

            this.InsertGroupDealProduct(groupDealProduct);
            // generic attributes for the groupdeal product
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.IsGroupDeal, true);
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.Active, true);
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.SeName, "dummy-SeName");
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.CouponCode, "12345");
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.Country, "Pakistan");
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.StateOrProvince, "KPK");
            _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.City, "Kamra");

            return groupDealProduct.Id;
        }

        public override IPagedList<Product> SearchProducts(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false,
            bool? overridePublished = null)
        {
            IList<int> filterableSpecificationAttributeOptionIds;
            var products = base.SearchProducts(out filterableSpecificationAttributeOptionIds, false,
                pageIndex, pageSize, categoryIds, manufacturerId,
                storeId, vendorId, warehouseId,
                productType, visibleIndividuallyOnly, featuredProducts,
                priceMin, priceMax, productTagId, keywords, searchDescriptions, searchSku,
                searchProductTags, languageId, filteredSpecs,
                orderBy, showHidden, overridePublished);

            var query = products.Where(p => p.IsGroupDeal());
            var groupDealProducts = new PagedList<Product>(query, products.PageIndex, products.PageSize, products.TotalCount);

            return groupDealProducts;
        }
    }
}
