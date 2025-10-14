[web] GET /api/accounting/ledger/cashflow/categories/{id}  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.GetCategory)  [L46–L55] [auth=Authentication.UserPolicy]
  └─ maps_to CashflowCategoryDto [L54]
  └─ maps_to CashflowCategoryDto [L49]
    └─ automapper.registration CirrusMappingProfile (CashflowCategory->CashflowCategoryDto) [L468]
  └─ calls CashflowCategoryRepository.ReadQuery [L49]
  └─ queries CashflowCategory [L49]
    └─ reads_from CashflowCategories
  └─ uses_service IControlledRepository<CashflowCategory>
    └─ method ReadQuery [L49]
  └─ uses_service IMapper
    └─ method Map [L54]
  └─ sends_request CanIAccessFileQuery [L52]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessFileQueryHandler.Handle [L43–L101]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L66]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L90]
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L68]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L68]
      └─ uses_cache IDistributedCache [L79]
        └─ method SetRecordAsync [write] [L79]
      └─ uses_cache IDistributedCache [L71]
        └─ method DoesRecordExistAsync [access] [L71]
      └─ uses_cache IDistributedCache [L68]
        └─ method CreateAccessKey [write] [L68]

