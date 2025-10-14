[web] GET /api/accounting/ledger/cashflow/lines/  (Cirrus.Api.Controllers.Accounting.Ledger.Cashflow.CashflowLinesController.GetLines)  [L53–L63] [auth=Authentication.UserPolicy]
  └─ maps_to CashflowLineDto [L58]
    └─ automapper.registration CirrusMappingProfile (CashflowLine->CashflowLineDto) [L470]
  └─ calls CashflowLineRepository.ReadQuery [L58]
  └─ queries CashflowLine [L58]
    └─ reads_from CashflowLines
  └─ uses_service IControlledRepository<CashflowLine>
    └─ method ReadQuery [L58]
  └─ sends_request CanIAccessFileQuery [L56]
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

