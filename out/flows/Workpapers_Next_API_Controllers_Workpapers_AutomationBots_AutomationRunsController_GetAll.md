[web] GET /api/binders/{binderId:guid}/automation-runs  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.GetAll)  [L44–L50] [auth=AuthorizationPolicies.User]
  └─ sends_request CanIAccessBinderQuery [L47]
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
  └─ sends_request GetAutomationRunsQuery [L49]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetAutomationRunsQueryHandler.Handle [L35–L93]
      └─ maps_to AutomationRunLightDto [L56]
        └─ automapper.registration WorkpapersMappingProfile (AutomationRun->AutomationRunLightDto) [L914]
      └─ maps_to AutomationRunStageDto [L67]
        └─ converts_to AutomationRunStageModel [L961]
      └─ uses_service BotService
        └─ method GetStageDefinitions [L82]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method ReadQuery [L56]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L62]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L59]

