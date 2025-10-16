[web] PUT /api/source-accounts/move  (Workpapers.Next.API.Controllers.SourceAccountsController.Move)  [L292–L300] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request MoveAccountsCommand -> MoveAccountsCommandHandler [L297]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Ledger.MoveAccountsCommandHandler.Handle [L55–L305]
      └─ uses_service ILeadScheduleService (AddScoped)
        └─ method HasParentLeadSchedule [L198]
          └─ implementation Cirrus.ApplicationService.Features.LeadSchedules.LeadScheduleService.HasParentLeadSchedule [L14-L50]
            └─ uses_service IControlledRepository<StandardAccount> (Scoped (inferred))
              └─ method GetAccountsInHierarchy [L27]
                └─ implementation Cirrus.Data.Repository.Accounting.Ledger.StandardAccountRepository.GetAccountsInHierarchy
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L152]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Account> (Scoped (inferred))
        └─ method ReadQuery [L80]
          └─ implementation Cirrus.Data.Repository.Accounting.Ledger.AccountRepository.ReadQuery
      └─ logs ILogger [Warning] [L248]
  └─ impact_summary
    └─ requests 1
      └─ MoveAccountsCommand
    └─ handlers 1
      └─ MoveAccountsCommandHandler

