[web] GET /api/accounting/ledger/accounts/{fileId:Guid}/hierarchy/refresh  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.RefreshAccountHierarchy)  [L212–L247] [auth=user]
  └─ calls FileRepository.ReadQuery [L218]
  └─ calls AccountRepository.ReadQuery [L224]
  └─ queries File [L218]
    └─ reads_from Files
  └─ queries Account [L224]
  └─ uses_service IControlledRepository<Account>
    └─ method ReadQuery [L224]
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L218]
  └─ sends_request CanIAccessFileQuery [L215]
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

