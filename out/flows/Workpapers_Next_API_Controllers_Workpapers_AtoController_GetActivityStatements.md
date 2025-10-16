[web] GET /api/ato/activity-statements  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetActivityStatements)  [L34–L40] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetActivityStatementsQuery [L37]
    └─ handled_by GovLink.Application.Queries.ActivityStatements.GetActivityStatementQueryHandler.Handle [L90–L198]
      └─ uses_client IAtoClient [L110]
      └─ uses_service IAtoClient (AddScoped)
        └─ method RequestAsync [L110]
          └─ ... (no implementation details available)
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetActivityStatementsQueryHandler.Handle [L32–L66]
      └─ uses_client DataGetClient [L60]
        └─ calls GetActivityStatementsDetail (GET /api/gov-link/activity-statements/detail?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetActivityStatementsAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L214]
          └─ target_service DataGet.Api
            └─ [web] GET /api/gov-link/activity-statements/detail  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementsDetail)  [L33–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
      └─ uses_service DataGetClient
        └─ method GetActivityStatementsAsync [L60]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetActivityStatementsAsync [L32-L506]
            └─ calls GetActivityStatementsDetail [L214]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L52]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

