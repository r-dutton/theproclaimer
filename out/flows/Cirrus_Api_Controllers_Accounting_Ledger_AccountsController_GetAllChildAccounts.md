[web] GET /api/accounting/ledger/accounts/child-accounts  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetAllChildAccounts)  [L90–L101] [auth=user]
  └─ maps_to AccountLightDto [L95]
    └─ automapper.registration CirrusMappingProfile (Account->AccountLightDto) [L313]
  └─ calls AccountRepository.ReadQuery [L95]
  └─ queries Account [L95]
  └─ uses_service IControlledRepository<Account>
    └─ method ReadQuery [L95]
  └─ sends_request CanIAccessFileQuery [L94]
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

