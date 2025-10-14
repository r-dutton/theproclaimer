[web] PUT /api/accounting/ledger/cashflow/categories/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowCategoriesController.Reorder)  [L117–L132] [auth=Authentication.UserPolicy]
  └─ calls CashflowCategoryRepository.WriteQuery [L120]
  └─ writes_to CashflowCategory [L120]
    └─ reads_from CashflowCategories
  └─ uses_service IControlledRepository<CashflowCategory>
    └─ method WriteQuery [L120]
  └─ uses_service IReadRepository (InstancePerLifetimeScope)
    └─ method Table [L125]
  └─ sends_request CanIAccessFileQuery [L123]
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

