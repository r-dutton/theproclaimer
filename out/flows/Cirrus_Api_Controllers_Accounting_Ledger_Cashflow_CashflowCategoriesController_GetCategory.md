[web] GET /api/accounting/ledger/cashflow/categories/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.GetCategory)  [L46–L55] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to CashflowCategoryDto [L54]
  └─ maps_to CashflowCategoryDto [L49]
    └─ automapper.registration CirrusMappingProfile (CashflowCategory->CashflowCategoryDto) [L468]
  └─ calls CashflowCategoryRepository.ReadQuery [L49]
  └─ query CashflowCategory [L49]
    └─ reads_from CashflowCategories
  └─ uses_service IControlledRepository<CashflowCategory>
    └─ method ReadQuery [L49]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L54]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessFileQuery [L52]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
          └─ implementation ITenantService.GetCurrentTenant [L14-L14]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L68]

