[web] POST /api/accounting/ledger/cashflow/categories/{id}/copy  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.CopyCategory)  [L86–L102] [auth=Authentication.UserPolicy]
  └─ calls CashflowCategoryRepository.Add [L101]
  └─ calls CashflowCategoryRepository.WriteQuery [L89]
  └─ writes_to CashflowCategory [L101]
    └─ reads_from CashflowCategories
  └─ writes_to CashflowCategory [L89]
    └─ reads_from CashflowCategories
  └─ uses_service IControlledRepository<CashflowCategory>
    └─ method WriteQuery [L89]
  └─ uses_service IReadRepository (InstancePerLifetimeScope)
    └─ method Table [L95]
  └─ sends_request CanIAccessFileQuery [L93]
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

