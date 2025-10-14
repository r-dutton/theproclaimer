[web] PUT /api/source-accounts/{id:Guid}  (Workpapers.Next.API.Controllers.SourceAccountsController.Update)  [L178–L184] [auth=AuthorizationPolicies.User]
  └─ sends_request UpdateAccountCommand [L181]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.SourceAccounts.UpdateAccountCommandHandler.Handle [L38–L192]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L59]
      └─ uses_service IControlledRepository<TransientAccountProperties>
        └─ method WriteQuery [L166]
      └─ uses_service LeadScheduleService
        └─ method HasParentLeadSchedule [L114]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]

