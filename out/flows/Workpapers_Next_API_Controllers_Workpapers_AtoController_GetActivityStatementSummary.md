[web] GET /api/ato/activity-statements/summary  (Workpapers.Next.API.Controllers.Workpapers.AtoController.GetActivityStatementSummary)  [L42–L48] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetActivityStatementSummaryQuery [L45]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GovLink.GetActivityStatementSummaryQueryHandler.Handle [L31–L64]
      └─ uses_client DataGetClient [L58]
        └─ calls GetActivityStatementSummary (GET /api/gov-link/activity-statements/summary?clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}, method=GetActivityStatementSummaryAsync, target=DataGet.Api, query=clientABN={*}&clientABNBranch={*}&clientId={*}&clientTFN={*}&endDate={*}&forceSync={*}&softwareSubscriptionId={*}&startDate={*}&taxAgentABN={*}&taxAgentNumber={*}) [L231]
          └─ target_service DataGet.Api
            └─ [web] GET /api/gov-link/activity-statements/summary  (DataGet.Api.Controllers.GovLink.ActivityStatementController.GetActivityStatementSummary)  [L44–L53] status=200 [auth=Authentication.MachineToMachinePolicy]
      └─ uses_service DataGetClient
        └─ method GetActivityStatementSummaryAsync [L58]
          └─ implementation Workpapers.Next.DataGet.Client.DataGetClient.GetActivityStatementSummaryAsync [L32-L506]
            └─ calls GetActivityStatementSummary [L231]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L50]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

