[web] PUT /api/accounting/ledger/accounts/{id:Guid}/unlink-standard  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.UnlinkStandardAccount)  [L534–L543] [auth=user]
  └─ calls AccountRepository.WriteQuery [L537]
  └─ writes_to Account [L537]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L537]
  └─ sends_request CanIAccessFileQuery [L541]
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

