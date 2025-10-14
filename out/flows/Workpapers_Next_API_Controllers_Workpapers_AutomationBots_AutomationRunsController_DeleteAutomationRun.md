[web] DELETE /api/binders/{binderId:guid}/automation-runs/{id:guid}  (Workpapers.Next.API.Controllers.Workpapers.AutomationBots.AutomationRunsController.DeleteAutomationRun)  [L92–L101] [auth=AuthorizationPolicies.User]
  └─ sends_request DeleteAutomationRunCommand [L98]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.AutomationBots.DeleteAutomationRunCommandHandler.Handle [L33–L82]
      └─ uses_service IControlledRepository<AutomationRun>
        └─ method WriteQuery [L57]
      └─ uses_service IControlledRepository<Binder>
        └─ method WriteQuery [L54]
      └─ uses_service JournalServiceFactory
        └─ method GetService [L75]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L79]
  └─ sends_request CanIAccessBinderQuery [L96]
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

