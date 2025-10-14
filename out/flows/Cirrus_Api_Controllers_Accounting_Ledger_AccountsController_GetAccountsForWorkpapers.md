[web] GET /api/accounting/ledger/accounts/{fileId:Guid}/workpapers-info  (Cirrus.Api.Controllers.Accounting.Ledger.AccountsController.GetAccountsForWorkpapers)  [L189–L195] [auth=user]
  └─ sends_request GetAccountsForWorkpapersQuery [L193]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Queries.GetAccountsForWorkpapersQueryHandler.Handle [L25–L74]
      └─ maps_to AccountInfoForWorkpapersDto [L40]
        └─ automapper.registration CirrusMappingProfile (Account->AccountInfoForWorkpapersDto) [L354]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L40]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L59]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L42]
  └─ sends_request CanIAccessFileQuery [L192]
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

