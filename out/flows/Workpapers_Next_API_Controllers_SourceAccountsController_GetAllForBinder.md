[web] GET /api/source-accounts/for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.SourceAccountsController.GetAllForBinder)  [L89–L107] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L94]
  └─ queries Binder [L94]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method ReadQuery [L94]
  └─ sends_request CanIAccessBinderQuery [L92]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L101]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
      └─ uses_service UserService
        └─ method GetUserId [L91]
      └─ uses_cache IDistributedCache [L121]
        └─ method SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache [L109]
        └─ method DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache [L92]
        └─ method CreateAccessKey [write] [L92]
  └─ sends_request GetSourceAccountsLightQuery [L105]
  └─ sends_request GetSourceAccountsQuery [L106]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceAccounts.GetSourceAccountsQueryHandler.Handle [L50–L99]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L78]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L92]
      └─ uses_service SourceAccountsQueryProcessor
        └─ method ProcessSourceAccounts [L95]

