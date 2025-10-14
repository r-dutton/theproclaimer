[web] PUT /api/sources/{type}/trial-balance  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetTrialBalance)  [L205–L223] [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L208]
  └─ writes_to Binder [L208]
    └─ reads_from Binders
  └─ uses_service IControlledRepository<Binder>
    └─ method WriteQuery [L208]
  └─ sends_request ImportFromApiCommand [L220]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.ImportFromApiCommandHandler.Handle [L60–L188]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L174]
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L125]
      └─ uses_service IControlledRepository<Journal>
        └─ method Add [L154]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L104]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L168]
  └─ sends_request CanIAccessBinderQuery [L212]
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

