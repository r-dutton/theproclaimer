[web] PUT /api/source-accounts/move  (Workpapers.Next.API.Controllers.SourceAccountsController.Move)  [L292–L300] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request MoveAccountsCommand [L297]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.MoveAccountsCommandHandler.Handle [L55–L305]
      └─ uses_service IControlledRepository<Account>
        └─ method ReadQuery [L80]
          └─ ... (no implementation details available)
      └─ uses_service ILeadScheduleService (AddScoped)
        └─ method HasParentLeadSchedule [L198]
          └─ implementation Cirrus.ApplicationService.Features.LeadSchedules.LeadScheduleService.HasParentLeadSchedule [L14-L50]
      └─ uses_service ILogger
        └─ method LogWarning [L248]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L152]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ logs ILogger [Warning] [L248]

