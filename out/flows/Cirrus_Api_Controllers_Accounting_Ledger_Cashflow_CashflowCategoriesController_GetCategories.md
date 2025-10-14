[web] GET /api/accounting/ledger/cashflow/categories/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.GetCategories)  [L57–L84] [auth=Authentication.UserPolicy]
  └─ maps_to CashflowCategoryLightDto [L79]
    └─ automapper.registration CirrusMappingProfile (CashflowCategory->CashflowCategoryLightDto) [L466]
  └─ maps_to CashflowCategoryLightDto [L70]
  └─ calls CashflowCategoryRepository.ReadQuery [L79]
  └─ queries CashflowCategory [L79]
    └─ reads_from CashflowCategories
  └─ uses_service IControlledRepository<CashflowCategory>
    └─ method ReadQuery [L79]
  └─ uses_service IReadRepository (InstancePerLifetimeScope)
    └─ method Table [L70]
  └─ sends_request CanIAccessFileQuery [L66]
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

