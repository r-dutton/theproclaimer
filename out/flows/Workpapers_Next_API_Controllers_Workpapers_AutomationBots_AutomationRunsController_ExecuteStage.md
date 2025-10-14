[web] PUT /api/binders/{binderId:guid}/automation-runs/{id:guid}/execute  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.ExecuteStage)  [L82–L90] [auth=AuthorizationPolicies.User]
  └─ sends_request ExecuteBotStageCommand [L87]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.AutomationBots.ExecuteBotStageCommandHandler.Handle [L49–L75]
      └─ maps_to AutomationRunDto [L73]
        └─ converts_to AutomationRunUpdateModel [L960]
      └─ maps_to AutomationRunDto [L69]
        └─ converts_to AutomationRunUpdateModel [L960]
      └─ uses_service IBotService (AddScoped)
        └─ method ExecuteStage [L68]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method WriteQuery [L64]
      └─ uses_service IMapper
        └─ method Map [L69]
  └─ sends_request CanIAccessBinderQuery [L85]
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

