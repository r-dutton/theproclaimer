[web] GET /api/binders/{binderId:guid}/automation-runs/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.Get)  [L34–L42] [auth=AuthorizationPolicies.User]
  └─ sends_request CanIAccessBinderQuery [L37]
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
  └─ sends_request GetAutomationRunLightQuery [L40]
  └─ sends_request GetAutomationRunQuery [L41]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.AutomationBots.GetAutomationRunQueryHandler.Handle [L49–L118]
      └─ maps_to AutomationRunDto [L70]
        └─ automapper.registration WorkpapersMappingProfile (AutomationRun->AutomationRunDto) [L920]
        └─ converts_to AutomationRunUpdateModel [L960]
      └─ maps_to AutomationRunLightDto [L85]
        └─ automapper.registration WorkpapersMappingProfile (AutomationRun->AutomationRunLightDto) [L914]
      └─ maps_to AutomationRunStageDto [L89]
        └─ converts_to AutomationRunStageModel [L961]
      └─ uses_service BotService
        └─ method GetStageDefinitions [L107]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method ReadQuery [L70]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L74]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L93]

