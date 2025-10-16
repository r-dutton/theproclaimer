[web] PUT /api/source-accounts/{id:Guid}  (Workpapers.Next.API.Controllers.SourceAccountsController.Update)  [L178–L184] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request UpdateAccountCommand -> UpdateAccountCommandHandler [L181]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.SourceAccounts.UpdateAccountCommandHandler.Handle [L38–L192]
      └─ calls SourceAccountRepository (methods: ReadQuery,WriteQuery) [L101]
      └─ uses_service IControlledRepository<TransientAccountProperties> (Scoped (inferred))
        └─ method WriteQuery [L166]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.TransientAccountPropertiesRepository.WriteQuery
      └─ uses_service LeadScheduleService
        └─ method HasParentLeadSchedule [L114]
          └─ implementation Workpapers.Next.ApplicationService.Features.LeadSchedules.LeadScheduleService.HasParentLeadSchedule [L15-L65]
            └─ calls SourceAccountRepository.ReadQuery [L47]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ UpdateAccountCommand
    └─ handlers 1
      └─ UpdateAccountCommandHandler

