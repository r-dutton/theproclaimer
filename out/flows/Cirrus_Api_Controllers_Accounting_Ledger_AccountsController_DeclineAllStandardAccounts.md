[web] PUT /api/accounting/ledger/accounts/{id:Guid}/decline-all-standards  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.DeclineAllStandardAccounts)  [L548–L557] [auth=user]
  └─ calls AccountRepository.WriteQuery [L551]
  └─ writes_to Account [L551]
  └─ uses_service IControlledRepository<Account>
    └─ method WriteQuery [L551]
  └─ sends_request CanIAccessFileQuery [L555]
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

