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

        public virtual IPagedList<Product> SearchGroupDeals(
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
            return SearchGroupDeals(out filterableSpecificationAttributeOptionIds, false,
                pageIndex, pageSize, categoryIds, manufacturerId,
                storeId, vendorId, warehouseId,
                productType, visibleIndividuallyOnly, featuredProducts,
                priceMin, priceMax, productTagId, keywords, searchDescriptions, searchSku,
                searchProductTags, languageId, filteredSpecs,
                orderBy, showHidden, overridePublished);
        }

        public virtual IPagedList<Product> SearchGroupDeals(
            out IList<int> filterableSpecificationAttributeOptionIds,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            int pageIndex = 0,
            int pageSize = 2147483647,  //Int32.MaxValue
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
            filterableSpecificationAttributeOptionIds = new List<int>();

            //search by keyword
            bool searchLocalizedValue = false;
            if (languageId > 0)
            {
                if (showHidden)
                {
                    searchLocalizedValue = true;
                }
                else
                {
                    //ensure that we have at least two published languages
                    var totalPublishedLanguages = _languageService.GetAllLanguages().Count;
                    searchLocalizedValue = totalPublishedLanguages >= 2;
                }
            }

            //validate "categoryIds" parameter
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            //Access control list. Allowed customer roles
            var allowedCustomerRolesIds = _workContext.CurrentCustomer.GetCustomerRoleIds();
            
            //if (_commonSettings.UseStoredProceduresIfSupported && _dataProvider.StoredProceduredSupported)
            if (false)
            {
                //stored procedures are enabled and supported by the database. 
                //It's much faster than the LINQ implementation below 

                #region Use stored procedure

                //pass category identifiers as comma-delimited string
                string commaSeparatedCategoryIds = categoryIds == null ? "" : string.Join(",", categoryIds);


                //pass customer role identifiers as comma-delimited string
                string commaSeparatedAllowedCustomerRoleIds = string.Join(",", allowedCustomerRolesIds);


                //pass specification identifiers as comma-delimited string
                string commaSeparatedSpecIds = "";
                if (filteredSpecs != null)
                {
                    ((List<int>)filteredSpecs).Sort();
                    commaSeparatedSpecIds = string.Join(",", filteredSpecs);
                }

                //some databases don't support int.MaxValue
                if (pageSize == int.MaxValue)
                    pageSize = int.MaxValue - 1;

                //prepare parameters
                var pCategoryIds = _dataProvider.GetParameter();
                pCategoryIds.ParameterName = "CategoryIds";
                pCategoryIds.Value = commaSeparatedCategoryIds != null ? (object)commaSeparatedCategoryIds : DBNull.Value;
                pCategoryIds.DbType = DbType.String;

                var pManufacturerId = _dataProvider.GetParameter();
                pManufacturerId.ParameterName = "ManufacturerId";
                pManufacturerId.Value = manufacturerId;
                pManufacturerId.DbType = DbType.Int32;

                var pStoreId = _dataProvider.GetParameter();
                pStoreId.ParameterName = "StoreId";
                pStoreId.Value = !_catalogSettings.IgnoreStoreLimitations ? storeId : 0;
                pStoreId.DbType = DbType.Int32;

                var pVendorId = _dataProvider.GetParameter();
                pVendorId.ParameterName = "VendorId";
                pVendorId.Value = vendorId;
                pVendorId.DbType = DbType.Int32;

                var pWarehouseId = _dataProvider.GetParameter();
                pWarehouseId.ParameterName = "WarehouseId";
                pWarehouseId.Value = warehouseId;
                pWarehouseId.DbType = DbType.Int32;

                var pProductTypeId = _dataProvider.GetParameter();
                pProductTypeId.ParameterName = "ProductTypeId";
                pProductTypeId.Value = productType.HasValue ? (object)productType.Value : DBNull.Value;
                pProductTypeId.DbType = DbType.Int32;

                var pVisibleIndividuallyOnly = _dataProvider.GetParameter();
                pVisibleIndividuallyOnly.ParameterName = "VisibleIndividuallyOnly";
                pVisibleIndividuallyOnly.Value = visibleIndividuallyOnly;
                pVisibleIndividuallyOnly.DbType = DbType.Int32;

                var pProductTagId = _dataProvider.GetParameter();
                pProductTagId.ParameterName = "ProductTagId";
                pProductTagId.Value = productTagId;
                pProductTagId.DbType = DbType.Int32;

                var pFeaturedProducts = _dataProvider.GetParameter();
                pFeaturedProducts.ParameterName = "FeaturedProducts";
                pFeaturedProducts.Value = featuredProducts.HasValue ? (object)featuredProducts.Value : DBNull.Value;
                pFeaturedProducts.DbType = DbType.Boolean;

                var pPriceMin = _dataProvider.GetParameter();
                pPriceMin.ParameterName = "PriceMin";
                pPriceMin.Value = priceMin.HasValue ? (object)priceMin.Value : DBNull.Value;
                pPriceMin.DbType = DbType.Decimal;

                var pPriceMax = _dataProvider.GetParameter();
                pPriceMax.ParameterName = "PriceMax";
                pPriceMax.Value = priceMax.HasValue ? (object)priceMax.Value : DBNull.Value;
                pPriceMax.DbType = DbType.Decimal;

                var pKeywords = _dataProvider.GetParameter();
                pKeywords.ParameterName = "Keywords";
                pKeywords.Value = keywords != null ? (object)keywords : DBNull.Value;
                pKeywords.DbType = DbType.String;

                var pSearchDescriptions = _dataProvider.GetParameter();
                pSearchDescriptions.ParameterName = "SearchDescriptions";
                pSearchDescriptions.Value = searchDescriptions;
                pSearchDescriptions.DbType = DbType.Boolean;

                var pSearchSku = _dataProvider.GetParameter();
                pSearchSku.ParameterName = "SearchSku";
                pSearchSku.Value = searchSku;
                pSearchSku.DbType = DbType.Boolean;

                var pSearchProductTags = _dataProvider.GetParameter();
                pSearchProductTags.ParameterName = "SearchProductTags";
                pSearchProductTags.Value = searchProductTags;
                pSearchProductTags.DbType = DbType.Boolean;

                var pUseFullTextSearch = _dataProvider.GetParameter();
                pUseFullTextSearch.ParameterName = "UseFullTextSearch";
                pUseFullTextSearch.Value = _commonSettings.UseFullTextSearch;
                pUseFullTextSearch.DbType = DbType.Boolean;

                var pFullTextMode = _dataProvider.GetParameter();
                pFullTextMode.ParameterName = "FullTextMode";
                pFullTextMode.Value = (int)_commonSettings.FullTextMode;
                pFullTextMode.DbType = DbType.Int32;

                var pFilteredSpecs = _dataProvider.GetParameter();
                pFilteredSpecs.ParameterName = "FilteredSpecs";
                pFilteredSpecs.Value = commaSeparatedSpecIds != null ? (object)commaSeparatedSpecIds : DBNull.Value;
                pFilteredSpecs.DbType = DbType.String;

                var pLanguageId = _dataProvider.GetParameter();
                pLanguageId.ParameterName = "LanguageId";
                pLanguageId.Value = searchLocalizedValue ? languageId : 0;
                pLanguageId.DbType = DbType.Int32;

                var pOrderBy = _dataProvider.GetParameter();
                pOrderBy.ParameterName = "OrderBy";
                pOrderBy.Value = (int)orderBy;
                pOrderBy.DbType = DbType.Int32;

                var pAllowedCustomerRoleIds = _dataProvider.GetParameter();
                pAllowedCustomerRoleIds.ParameterName = "AllowedCustomerRoleIds";
                pAllowedCustomerRoleIds.Value = commaSeparatedAllowedCustomerRoleIds;
                pAllowedCustomerRoleIds.DbType = DbType.String;

                var pPageIndex = _dataProvider.GetParameter();
                pPageIndex.ParameterName = "PageIndex";
                pPageIndex.Value = pageIndex;
                pPageIndex.DbType = DbType.Int32;

                var pPageSize = _dataProvider.GetParameter();
                pPageSize.ParameterName = "PageSize";
                pPageSize.Value = pageSize;
                pPageSize.DbType = DbType.Int32;

                var pShowHidden = _dataProvider.GetParameter();
                pShowHidden.ParameterName = "ShowHidden";
                pShowHidden.Value = showHidden;
                pShowHidden.DbType = DbType.Boolean;

                var pOverridePublished = _dataProvider.GetParameter();
                pOverridePublished.ParameterName = "OverridePublished";
                pOverridePublished.Value = overridePublished != null ? (object)overridePublished.Value : DBNull.Value;
                pOverridePublished.DbType = DbType.Boolean;

                var pLoadFilterableSpecificationAttributeOptionIds = _dataProvider.GetParameter();
                pLoadFilterableSpecificationAttributeOptionIds.ParameterName = "LoadFilterableSpecificationAttributeOptionIds";
                pLoadFilterableSpecificationAttributeOptionIds.Value = loadFilterableSpecificationAttributeOptionIds;
                pLoadFilterableSpecificationAttributeOptionIds.DbType = DbType.Boolean;

                var pFilterableSpecificationAttributeOptionIds = _dataProvider.GetParameter();
                pFilterableSpecificationAttributeOptionIds.ParameterName = "FilterableSpecificationAttributeOptionIds";
                pFilterableSpecificationAttributeOptionIds.Direction = ParameterDirection.Output;
                pFilterableSpecificationAttributeOptionIds.Size = int.MaxValue - 1;
                pFilterableSpecificationAttributeOptionIds.DbType = DbType.String;

                var pTotalRecords = _dataProvider.GetParameter();
                pTotalRecords.ParameterName = "TotalRecords";
                pTotalRecords.Direction = ParameterDirection.Output;
                pTotalRecords.DbType = DbType.Int32;

                //invoke stored procedure
                var products = _dbContext.ExecuteStoredProcedureList<Product>(
                    "ProductLoadAllPaged",
                    pCategoryIds,
                    pManufacturerId,
                    pStoreId,
                    pVendorId,
                    pWarehouseId,
                    pProductTypeId,
                    pVisibleIndividuallyOnly,
                    pProductTagId,
                    pFeaturedProducts,
                    pPriceMin,
                    pPriceMax,
                    pKeywords,
                    pSearchDescriptions,
                    pSearchSku,
                    pSearchProductTags,
                    pUseFullTextSearch,
                    pFullTextMode,
                    pFilteredSpecs,
                    pLanguageId,
                    pOrderBy,
                    pAllowedCustomerRoleIds,
                    pPageIndex,
                    pPageSize,
                    pShowHidden,
                    pOverridePublished,
                    pLoadFilterableSpecificationAttributeOptionIds,
                    pFilterableSpecificationAttributeOptionIds,
                    pTotalRecords);
                //get filterable specification attribute option identifier
                string filterableSpecificationAttributeOptionIdsStr = (pFilterableSpecificationAttributeOptionIds.Value != DBNull.Value) ? (string)pFilterableSpecificationAttributeOptionIds.Value : "";
                if (loadFilterableSpecificationAttributeOptionIds &&
                    !string.IsNullOrWhiteSpace(filterableSpecificationAttributeOptionIdsStr))
                {
                    filterableSpecificationAttributeOptionIds = filterableSpecificationAttributeOptionIdsStr
                       .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(x => Convert.ToInt32(x.Trim()))
                       .ToList();
                }
                //return products
                int totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
                return new PagedList<Product>(products, pageIndex, pageSize, totalRecords);

                #endregion
            }
            else
            {
                //stored procedures aren't supported. Use LINQ

                #region Search products

                //groupdeal products
                var query = _productRepository.Table;
                query = query.Where(p => !p.Deleted);
                if (!overridePublished.HasValue)
                {
                    //process according to "showHidden"
                    if (!showHidden)
                    {
                        query = query.Where(p => p.Published);
                    }
                }
                else if (overridePublished.Value)
                {
                    //published only
                    query = query.Where(p => p.Published);
                }
                else if (!overridePublished.Value)
                {
                    //unpublished only
                    query = query.Where(p => !p.Published);
                }
                if (visibleIndividuallyOnly)
                {
                    query = query.Where(p => p.VisibleIndividually);
                }
                if (productType.HasValue)
                {
                    var productTypeId = (int)productType.Value;
                    query = query.Where(p => p.ProductTypeId == productTypeId);
                }

                //The function 'CurrentUtcDateTime' is not supported by SQL Server Compact. 
                //That's why we pass the date value
                var nowUtc = DateTime.UtcNow;
                if (priceMin.HasValue)
                {
                    //min price
                    query = query.Where(p =>
                                        //special price (specified price and valid date range)
                                        ((p.SpecialPrice.HasValue &&
                                          ((!p.SpecialPriceStartDateTimeUtc.HasValue ||
                                            p.SpecialPriceStartDateTimeUtc.Value < nowUtc) &&
                                           (!p.SpecialPriceEndDateTimeUtc.HasValue ||
                                            p.SpecialPriceEndDateTimeUtc.Value > nowUtc))) &&
                                         (p.SpecialPrice >= priceMin.Value))
                                        ||
                                        //regular price (price isn't specified or date range isn't valid)
                                        ((!p.SpecialPrice.HasValue ||
                                          ((p.SpecialPriceStartDateTimeUtc.HasValue &&
                                            p.SpecialPriceStartDateTimeUtc.Value > nowUtc) ||
                                           (p.SpecialPriceEndDateTimeUtc.HasValue &&
                                            p.SpecialPriceEndDateTimeUtc.Value < nowUtc))) &&
                                         (p.Price >= priceMin.Value)));
                }
                if (priceMax.HasValue)
                {
                    //max price
                    query = query.Where(p =>
                                        //special price (specified price and valid date range)
                                        ((p.SpecialPrice.HasValue &&
                                          ((!p.SpecialPriceStartDateTimeUtc.HasValue ||
                                            p.SpecialPriceStartDateTimeUtc.Value < nowUtc) &&
                                           (!p.SpecialPriceEndDateTimeUtc.HasValue ||
                                            p.SpecialPriceEndDateTimeUtc.Value > nowUtc))) &&
                                         (p.SpecialPrice <= priceMax.Value))
                                        ||
                                        //regular price (price isn't specified or date range isn't valid)
                                        ((!p.SpecialPrice.HasValue ||
                                          ((p.SpecialPriceStartDateTimeUtc.HasValue &&
                                            p.SpecialPriceStartDateTimeUtc.Value > nowUtc) ||
                                           (p.SpecialPriceEndDateTimeUtc.HasValue &&
                                            p.SpecialPriceEndDateTimeUtc.Value < nowUtc))) &&
                                         (p.Price <= priceMax.Value)));
                }
                if (!showHidden)
                {
                    //available dates
                    query = query.Where(p =>
                        (!p.AvailableStartDateTimeUtc.HasValue || p.AvailableStartDateTimeUtc.Value < nowUtc) &&
                        (!p.AvailableEndDateTimeUtc.HasValue || p.AvailableEndDateTimeUtc.Value > nowUtc));
                }

                //searching by keyword
                if (!String.IsNullOrWhiteSpace(keywords))
                {
                    query = from p in query
                            join lp in _localizedPropertyRepository.Table on p.Id equals lp.EntityId into p_lp
                            from lp in p_lp.DefaultIfEmpty()
                            from pt in p.ProductTags.DefaultIfEmpty()
                            where (p.Name.Contains(keywords)) ||
                                  (searchDescriptions && p.ShortDescription.Contains(keywords)) ||
                                  (searchDescriptions && p.FullDescription.Contains(keywords)) ||
                                  (searchProductTags && pt.Name.Contains(keywords)) ||
                                  //localized values
                                  (searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "Name" && lp.LocaleValue.Contains(keywords)) ||
                                  (searchDescriptions && searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "ShortDescription" && lp.LocaleValue.Contains(keywords)) ||
                                  (searchDescriptions && searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "FullDescription" && lp.LocaleValue.Contains(keywords))
                            select p;
                }

                if (!showHidden && !_catalogSettings.IgnoreAcl)
                {
                    //ACL (access control list)
                    query = from p in query
                            join acl in _aclRepository.Table
                            on new { c1 = p.Id, c2 = "Product" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into p_acl
                            from acl in p_acl.DefaultIfEmpty()
                            where !p.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                            select p;
                }

                if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
                {
                    //Store mapping
                    query = from p in query
                            join sm in _storeMappingRepository.Table
                            on new { c1 = p.Id, c2 = "Product" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into p_sm
                            from sm in p_sm.DefaultIfEmpty()
                            where !p.LimitedToStores || storeId == sm.StoreId
                            select p;
                }

                //search by specs
                if (filteredSpecs != null && filteredSpecs.Count > 0)
                {
                    query = from p in query
                            where !filteredSpecs
                                       .Except(
                                           p.ProductSpecificationAttributes.Where(psa => psa.AllowFiltering).Select(
                                               psa => psa.SpecificationAttributeOptionId))
                                       .Any()
                            select p;
                }

                //category filtering
                if (categoryIds != null && categoryIds.Count > 0)
                {
                    query = from p in query
                            from pc in p.ProductCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
                            where (!featuredProducts.HasValue || featuredProducts.Value == pc.IsFeaturedProduct)
                            select p;
                }

                //manufacturer filtering
                if (manufacturerId > 0)
                {
                    query = from p in query
                            from pm in p.ProductManufacturers.Where(pm => pm.ManufacturerId == manufacturerId)
                            where (!featuredProducts.HasValue || featuredProducts.Value == pm.IsFeaturedProduct)
                            select p;
                }

                //vendor filtering
                if (vendorId > 0)
                {
                    query = query.Where(p => p.VendorId == vendorId);
                }

                //warehouse filtering
                if (warehouseId > 0)
                {
                    var manageStockInventoryMethodId = (int)ManageInventoryMethod.ManageStock;
                    query = query.Where(p =>
                        //"Use multiple warehouses" enabled
                        //we search in each warehouse
                        (p.ManageInventoryMethodId == manageStockInventoryMethodId &&
                         p.UseMultipleWarehouses &&
                         p.ProductWarehouseInventory.Any(pwi => pwi.WarehouseId == warehouseId))
                        ||
                        //"Use multiple warehouses" disabled
                        //we use standard "warehouse" property
                        ((p.ManageInventoryMethodId != manageStockInventoryMethodId ||
                          !p.UseMultipleWarehouses) &&
                          p.WarehouseId == warehouseId));
                }

                //related products filtering
                //if (relatedToProductId > 0)
                //{
                //    query = from p in query
                //            join rp in _relatedProductRepository.Table on p.Id equals rp.ProductId2
                //            where (relatedToProductId == rp.ProductId1)
                //            select p;
                //}

                //tag filtering
                if (productTagId > 0)
                {
                    query = from p in query
                            from pt in p.ProductTags.Where(pt => pt.Id == productTagId)
                            select p;
                }

                //only distinct products (group by ID)
                //if we use standard Distinct() method, then all fields will be compared (low performance)
                //it'll not work in SQL Server Compact when searching products by a keyword)
                query = from p in query
                        group p by p.Id
                        into pGroup
                        orderby pGroup.Key
                        select pGroup.FirstOrDefault();

                //sort products
                if (orderBy == ProductSortingEnum.Position && categoryIds != null && categoryIds.Count > 0)
                {
                    //category position
                    var firstCategoryId = categoryIds[0];
                    query = query.OrderBy(p => p.ProductCategories.FirstOrDefault(pc => pc.CategoryId == firstCategoryId).DisplayOrder);
                }
                else if (orderBy == ProductSortingEnum.Position && manufacturerId > 0)
                {
                    //manufacturer position
                    query =
                        query.OrderBy(p => p.ProductManufacturers.FirstOrDefault(pm => pm.ManufacturerId == manufacturerId).DisplayOrder);
                }
                else if (orderBy == ProductSortingEnum.Position)
                {
                    //otherwise sort by name
                    query = query.OrderBy(p => p.Name);
                }
                else if (orderBy == ProductSortingEnum.NameAsc)
                {
                    //Name: A to Z
                    query = query.OrderBy(p => p.Name);
                }
                else if (orderBy == ProductSortingEnum.NameDesc)
                {
                    //Name: Z to A
                    query = query.OrderByDescending(p => p.Name);
                }
                else if (orderBy == ProductSortingEnum.PriceAsc)
                {
                    //Price: Low to High
                    query = query.OrderBy(p => p.Price);
                }
                else if (orderBy == ProductSortingEnum.PriceDesc)
                {
                    //Price: High to Low
                    query = query.OrderByDescending(p => p.Price);
                }
                else if (orderBy == ProductSortingEnum.CreatedOn)
                {
                    //creation date
                    query = query.OrderByDescending(p => p.CreatedOnUtc);
                }
                else
                {
                    //actually this code is not reachable
                    query = query.OrderBy(p => p.Name);
                }

                // both of the following methods work
                // method 1 of getting groupdeals
                // methods are not recognized by IQueriable, so first conver to List(),
                // and then filter the products to get group deals only
                //var allGroupDeals = query.ToList().Where(p => p.IsGroupDeal()).ToList();
                //var groupDeals = new PagedList<Product>(allGroupDeals, pageIndex, pageSize);

                // method 2 of getting groupdeals
                var groupDealIds = this.GetAllGroupDealProducts().Select(gd => gd.Id);
                query = query.Where(p => groupDealIds.Contains(p.Id));
                var groupDeals = new PagedList<Product>(query, pageIndex, pageSize);

                //get filterable specification attribute option identifier
                if (loadFilterableSpecificationAttributeOptionIds)
                {
                    var querySpecs = from p in query
                                     join psa in _productSpecificationAttributeRepository.Table on p.Id equals psa.ProductId
                                     where psa.AllowFiltering
                                     select psa.SpecificationAttributeOptionId;
                    //only distinct attributes
                    filterableSpecificationAttributeOptionIds = querySpecs
                        .Distinct()
                        .ToList();
                }

                //return groupDeals
                return groupDeals;

                #endregion
            }
        }
    }
}
