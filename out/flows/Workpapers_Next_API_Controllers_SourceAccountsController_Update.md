[web] PUT /api/source-accounts/{id:Guid}  (Workpapers.Next.API.Controllers.SourceAccountsController.Update)  [L178–L184] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request UpdateAccountCommand [L181]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.SourceData.SourceAccounts.UpdateAccountCommandHandler.Handle [L38–L192]
      └─ uses_service LeadScheduleService
        └─ method HasParentLeadSchedule [L114]
          └─ implementation Workpapers.Next.ApplicationService.Features.LeadSchedules.LeadScheduleService.HasParentLeadSchedule [L15-L65]
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method WriteQuery [L59]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<TransientAccountProperties>
        └─ method WriteQuery [L166]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

