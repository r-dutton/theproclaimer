[web] PUT /api/standard-accounts/move  (Workpapers.Next.API.Controllers.Workpapers.StandardAccountsController.Move)  [L166–L180] [auth=AuthorizationPolicies.Administrator]
  └─ sends_request MoveStandardAccountsCommand [L177]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.MoveStandardAccountsCommandHandler.Handle [L46–L284]
      └─ uses_service IControlledRepository<MasterAccount>
        └─ method ReadQuery [L158]
      └─ uses_service IControlledRepository<StandardAccount>
        └─ method ReadQuery [L75]
      └─ uses_service ILeadScheduleService (AddScoped)
        └─ method HasParentLeadSchedule [L196]
      └─ uses_service ILogger
        └─ method LogWarning [L250]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L126]

