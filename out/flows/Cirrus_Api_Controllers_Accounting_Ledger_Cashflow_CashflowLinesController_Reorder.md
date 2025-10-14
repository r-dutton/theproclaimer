[web] PUT /api/accounting/ledger/cashflow/lines/{id}/reorder  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowLinesController.Reorder)  [L87–L114] [auth=Authentication.UserPolicy]
  └─ calls CashflowLineRepository.ReadQuery [L102]
  └─ calls CashflowLineRepository.WriteQuery [L90]
  └─ queries CashflowLine [L102]
    └─ reads_from CashflowLines
  └─ writes_to CashflowLine [L90]
    └─ reads_from CashflowLines
  └─ uses_service IControlledRepository<CashflowLine>
    └─ method WriteQuery [L90]
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

