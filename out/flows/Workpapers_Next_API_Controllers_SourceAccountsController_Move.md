[web] PUT /api/source-accounts/move  (Workpapers.Next.API.Controllers.SourceAccountsController.Move)  [L292–L300] [auth=AuthorizationPolicies.User]
  └─ sends_request MoveAccountsCommand [L297]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.MoveAccountsCommandHandler.Handle [L55–L305]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L80]
      └─ uses_service ILeadScheduleService (AddScoped)
        └─ method HasParentLeadSchedule [L198]
      └─ uses_service ILogger
        └─ method LogWarning [L248]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L152]

